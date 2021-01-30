using CefSharp;
using CefSharp.WinForms;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace FlowEngine.Workflow
{
    public class WorkflowDefinition
    {
        public string PageUrl { get; set; }

        public ChromiumWebBrowser Browser { get; set; }

        public Task<string> LoadPageAsync(string address = null)
        {
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
