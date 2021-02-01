using CefSharp;
using CefSharp.WinForms;
using IDST.AFlow.Browser.UI.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IDST.AFlow.Browser.UI.WorkflowHelpers
{
    //public ChromiumWebBrowser Browser { get; set; }
    public static class BrowserMethods
    {
        public static Task<string> MockLoadPageAsync(IntPtr browserRef, string address = null)
        {
            var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

            Task.Run(() => {
                Task.Delay(3000);
                tcs.TrySetResult("This is some mocked up HTML");
            });

            return tcs.Task;
        }

        public static Task<string> LoadPageAsync(IntPtr browserRef, string address = null)
        {
            var bRec = BrowserService.BrowserById(browserRef);
            if (bRec == null) { return null; }

            var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

            EventHandler<LoadingStateChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                //Wait for while page to finish loading not just the first frame
                if (!args.IsLoading)
                {
                    //xxxxxxxxx;
                    bRec.BrowserControl.LoadingStateChanged -= handler;
                    //Important that the continuation runs async using TaskCreationOptions.RunContinuationsAsynchronously
                    //string src = bRec.BrowserControl.GetMainFrame().GetSourceAsync().Result;
                    string src = "Mock HTML goes here";
                    //Browser.InvokeOnUiThreadIfRequired(() => Browser.GetMainFrame().GetSourceAsync().Result);

                    System.Diagnostics.Debug.WriteLine($"Got Page Source: {src}");
                    tcs.TrySetResult(src);
                }
            };
            bRec.BrowserControl.LoadingStateChanged += handler;

            if (!string.IsNullOrEmpty(address))
            {
                bRec.BrowserControl.InvokeOnUiThreadIfRequired(() => bRec.BrowserControl.Load(address));
            }
            return tcs.Task;
        }
    }
}
