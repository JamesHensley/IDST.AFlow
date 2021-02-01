using System;
using System.Collections.Generic;
using System.Text;

namespace IDST.AFlow.Browser.UI.Workflow.Models
{
    public class WorkflowData
    {
        public IntPtr BrowserHandle { get; set; }

        public string NavigateUrl { get; set; }

        public List<KeyValuePair<string, string>> OutputData { get; set; }
    }
}
