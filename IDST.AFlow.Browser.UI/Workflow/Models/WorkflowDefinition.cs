using System;
using System.Threading.Tasks;

namespace IDST.AFlow.Browser.UI.Workflow.Models
{
    public class WorkflowDefinition
    {
        public string PageUrl { get; set; }

        public IntPtr BrowserHandle { get; set; }
    }
}
