using CefSharp;
using System;
using System.Threading.Tasks;

namespace IDST.AFlow.Browser.UI.WorkflowHelpers
{
    public static class BrowserMethods
    {
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


        public static Task<string> ExecuteJSAsync(IntPtr browserRef, string scriptStr) {
            var bRec = BrowserService.BrowserById(browserRef);
            if (bRec == null || bRec.BrowserControl.CanExecuteJavascriptInMainFrame == false) { return null; }

            //Important that the continuation runs async using TaskCreationOptions.RunContinuationsAsynchronously
            var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

            bRec.BrowserControl.InvokeOnUiThreadIfRequired(() => {
                var result = bRec.BrowserControl.EvaluateScriptAsync(scriptStr).Result;
                string resultStr = System.Text.Json.JsonSerializer.Serialize(result);

                tcs.TrySetResult(resultStr);
            });

            return tcs.Task;
        }
    }
}
