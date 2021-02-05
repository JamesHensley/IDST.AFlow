using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace IDST.AFlow.Browser.UI.Workflow.Models
{
    public class WorkflowPersistentData {
        /// <summary>
        /// Contains the data passed between workflows
        /// </summary>
        public List<KeyValuePair<string, string>> PageData { get; set; }

        /// <summary>
        /// Represents the final data
        /// </summary>
        public List<ExpandoObject> FinalList { get; set; }

        /// <summary>
        /// Contains a list of all the columns in the FinalList
        /// </summary>
        public List<string> FinalColumns { get; set; }

        public ExpandoObject ScriptResult { get; set; }
    }

    public class WorkflowData
    {
        public IntPtr BrowserHandle { get; set; }

        public WorkflowPersistentData PersistentData { get; set; }

    }
}
