using IDST.AFlow.Browser.UI.WorkflowHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace IDST.AFlow.Browser.UI.Workflow.Steps
{
    public class HtmlStepNavigate : StepBody
    {
        public string NavigateUrl { get; set; }

        public IntPtr BrowserHandle { get; set; }

        //public string PageData { get; set; }
        public List<KeyValuePair<string, string>> PageData { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            PageData = new List<KeyValuePair<string, string>>();

            var pageKvpVal = BrowserMethods.LoadPageAsync(BrowserHandle, NavigateUrl).Result;
            PageData.Add(new KeyValuePair<string, string>("PageData", pageKvpVal));

            return ExecutionResult.Next();
        }
    }
}
