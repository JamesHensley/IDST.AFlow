using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Models;

namespace IDST.AFlow.Browser.UI.Workflow.Models
{
    public class WorkflowStepException
    {
        public string ExtraDetail { get; set; }

        public WorkflowInstance workflow { get; set; }

        public WorkflowStep step { get; set; }

        public Exception exception { get; set; }
    }
}
