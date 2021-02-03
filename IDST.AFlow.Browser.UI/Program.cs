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
using IDST.AFlow.Browser.UI.Services;
using IDST.AFlow.Browser.UI.Workflow;
using IDST.AFlow.Browser.UI.Workflow.Models;
using IDST.AFlow.Browser.UI.Workflow.Steps;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Services;

namespace IDST.AFlow.Browser.UI
{
    public class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            IServiceProvider serviceProvider = ServiceManager.ConfigureServices();
            //TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

            Cef.EnableHighDPISupport();

            IBrowserProcessHandler browserProcessHandler = new BrowserProcessHandler();

            //When multiThreadedMessageLoop = true then externalMessagePump must be set to false
            // To enable externalMessagePump set  multiThreadedMessageLoop = false and externalMessagePump = true
            var settings = new CefSettings() {
                MultiThreadedMessageLoop = true,
                ExternalMessagePump = false
            };

            CefExample.Init(settings, browserProcessHandler: browserProcessHandler);

            var form = serviceProvider.GetRequiredService<BrowserForm>();
            Application.Run(form);
            //var browser = new BrowserForm(serviceProvider);
            //Application.Run(browser);

            return 0;
        }
    }
}
