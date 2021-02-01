using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace IDST.AFlow.Workflow.Steps
{
    public class HtmlStepNavigate : StepBody
    {
        public string NavigateUrl { get; set; }

        public IntPtr BrowserHandle { get; set; }

        public string PageData { get; set; }


        public override ExecutionResult Run(IStepExecutionContext context)
        {
            PageData = "This would be some page data";
            return ExecutionResult.Next();
        }
    }
}
