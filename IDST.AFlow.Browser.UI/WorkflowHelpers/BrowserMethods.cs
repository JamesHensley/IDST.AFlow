using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IDST.AFlow.Browser.UI.WorkflowHelpers
{
    //public ChromiumWebBrowser Browser { get; set; }
    public static class BrowserMethods
    {
        public static Task<string> LoadPageAsync(IntPtr browserRef, string address = null)
        {
            ChromiumWebBrowser Browser = BrowserService.BrowserById(browserRef);
            if (Browser == null) {
                return null;
            }

            var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

            EventHandler<LoadingStateChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                //Wait for while page to finish loading not just the first frame
                if (!args.IsLoading)
                {
                    Browser.LoadingStateChanged -= handler;
                    //Important that the continuation runs async using TaskCreationOptions.RunContinuationsAsynchronously
                    tcs.TrySetResult(Browser.GetMainFrame().GetSourceAsync().Result);
                }
            };

            Browser.LoadingStateChanged += handler;

            if (!string.IsNullOrEmpty(address))
            {
                Browser.Load(address);
            }
            return tcs.Task;
        }
    }
}
