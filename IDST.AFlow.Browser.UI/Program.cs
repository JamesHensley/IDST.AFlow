// Copyright © 2010 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using IDST.AFlow.Browser.Core;
using IDST.AFlow.Browser.Core.Handlers;
using IDST.AFlow.Browser.UI.Forms;
using IDST.AFlow.Browser.UI.Handlers;
using IDST.AFlow.Browser.UI.Minimal;
using IDST.AFlow.Browser.UI.Workflow.Steps;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using WorkflowCore.Services;

namespace IDST.AFlow.Browser.UI
{
    public class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            IServiceProvider serviceProvider = ConfigureServices();

            const bool selfHostSubProcess = false;

            Cef.EnableHighDPISupport();

            //NOTE: Using a simple sub processes uses your existing application executable to spawn instances of the sub process.
            //Features like JSB, EvaluateScriptAsync, custom schemes require the CefSharp.BrowserSubprocess to function
            if (selfHostSubProcess)
            {
                var exitCode = CefSharp.BrowserSubprocess.SelfHost.Main(args);

                if (exitCode >= 0)
                {
                    return exitCode;
                }

#if DEBUG
                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    MessageBox.Show("When running this Example outside of Visual Studio " +
                                    "please make sure you compile in `Release` mode.", "Warning");
                }
#endif

                var settings = new CefSettings();
                settings.BrowserSubprocessPath = System.IO.Path.GetFullPath("IDST.AFlow.Browser.UI.exe");

                Cef.Initialize(settings);

                var browser = new SimpleBrowserForm(true);
                Application.Run(browser);
            }
            else
            {
#if DEBUG
                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    MessageBox.Show("When running this Example outside of Visual Studio " +
                                    "please make sure you compile in `Release` mode.", "Warning");
                }
#endif

                //When multiThreadedMessageLoop = true then externalMessagePump must be set to false
                // To enable externalMessagePump set  multiThreadedMessageLoop = false and externalMessagePump = true
                const bool multiThreadedMessageLoop = true;
                const bool externalMessagePump = false;

                var browser = new BrowserForm(multiThreadedMessageLoop, serviceProvider);
                //var browser = new SimpleBrowserForm(multiThreadedMessageLoop);
                //var browser = new TabulationDemoForm();

                IBrowserProcessHandler browserProcessHandler;

                if (multiThreadedMessageLoop)
                {
                    browserProcessHandler = new BrowserProcessHandler();
                }
                else
                {
                    if (externalMessagePump)
                    {
                        //Get the current taskScheduler (must be called after the form is created)
                        var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                        browserProcessHandler = new ScheduleMessagePumpBrowserProcessHandler(scheduler);
                    }
                    else
                    {
                        //We'll add out WinForms timer to the components container so it's Diposed
                        browserProcessHandler = new WinFormsBrowserProcessHandler(browser.Components);
                    }

                }

                var settings = new CefSettings();
                settings.MultiThreadedMessageLoop = multiThreadedMessageLoop;
                settings.ExternalMessagePump = externalMessagePump;

                CefExample.Init(settings, browserProcessHandler: browserProcessHandler);

                //Application.Run(new MultiFormAppContext(multiThreadedMessageLoop));
                Application.Run(browser);
            }

            return 0;
        }

        private static IServiceProvider ConfigureServices()
        {
            //setup dependency injection
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();
            services.AddTransient<HtmlStepNavigate>();

            //services.AddWorkflow(x => x.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WorkflowCore;Trusted_Connection=True;", true, true));
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

    }
}
