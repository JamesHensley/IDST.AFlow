using CefSharp;
using CefSharp.WinForms;
using System;
using System.Threading.Tasks;

namespace FlowEngine.Workflow
{
    public class WorkflowDefinition
    {
        public string PageUrl { get; set; }

        public IntPtr BrowserHandle { get; set; }
    }
}
