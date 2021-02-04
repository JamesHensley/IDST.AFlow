using CefSharp;
using System;
using System.Threading.Tasks;

namespace IDST.AFlow.Browser.UI.WorkflowHelpers
{
    public static class BrowserMethods
    {
        /// <summary>
        /// Navigates the browser to a URL and waits for loading to finish
        /// </summary>
        /// <returns>The source code of the web page loaded</returns>
        public static Task<string> LoadPageAsync(IntPtr browserRef, string address = null)
        {
            var bRec = BrowserService.BrowserById(browserRef);
            if (bRec == null) { return null; }

            //Important that the continuation runs async using TaskCreationOptions.RunContinuationsAsynchronously
            var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

            EventHandler<LoadingStateChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                //Wait for while page to finish loading not just the first frame
                if (!args.IsLoading)
                {
                    bRec.BrowserControl.LoadingStateChanged -= handler;
                    bRec.BrowserControl.InvokeOnUiThreadIfRequired(() => {
                        tcs.TrySetResult(bRec.BrowserControl.GetMainFrame().GetSourceAsync().Result);
                    });
                }
            };
            bRec.BrowserControl.LoadingStateChanged += handler;

            if (!string.IsNullOrEmpty(address))
            {
                bRec.BrowserControl.InvokeOnUiThreadIfRequired(() => bRec.BrowserControl.Load(address));
            }
            return tcs.Task;
        }

        /// <summary>
        /// Executes javascript and waits for the browser's IsLoading property to go FALSE; useful for
        /// performing SUBMIT on an HTML page and waiting for the results to load
        /// </summary>
        public static Task<bool> WaitForBrowserLoaded(IntPtr browserRef) {
            var bRec = BrowserService.BrowserById(browserRef);
            if (bRec == null || bRec.BrowserControl.CanExecuteJavascriptInMainFrame == false) { return null; }

            //Important that the continuation runs async using TaskCreationOptions.RunContinuationsAsynchronously
            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            EventHandler<LoadingStateChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                //Wait for while page to finish loading not just the first frame
                if (!args.IsLoading)
                {
                    tcs.TrySetResult(true);
                }
            };
            bRec.BrowserControl.LoadingStateChanged += handler;
            return tcs.Task;
        }

        /// <summary>
        /// Executes javascript and returns the result
        /// </summary>
        /// <returns>JavascriptResponse object with the results of the action</returns>
        public static Task<JavascriptResponse> ExecuteJS(IntPtr browserRef, string scriptStr) {
            var bRec = BrowserService.BrowserById(browserRef);
            if (bRec == null || bRec.BrowserControl.CanExecuteJavascriptInMainFrame == false) { return null; }

            //Important that the continuation runs async using TaskCreationOptions.RunContinuationsAsynchronously
            var tcs = new TaskCompletionSource<JavascriptResponse>(TaskCreationOptions.RunContinuationsAsynchronously);

            bRec.BrowserControl.InvokeOnUiThreadIfRequired(() => {
                var result = bRec.BrowserControl.EvaluateScriptAsync(scriptStr).Result;
                tcs.TrySetResult(result);
            });

            return tcs.Task;
        }
    }
}
