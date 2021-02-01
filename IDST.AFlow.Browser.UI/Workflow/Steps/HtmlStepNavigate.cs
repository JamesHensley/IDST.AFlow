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

        public List<KeyValuePair<string, string>> PageData { get; set; }


        public override ExecutionResult Run(IStepExecutionContext context)
        {
            System.Diagnostics.Debug.WriteLine($"Running Step: {NavigateUrl} {context?.Step?.Name ?? "No Name Given"}");
            var pageKvpVal = BrowserMethods.LoadPageAsync(BrowserHandle, NavigateUrl).Result;
            System.Diagnostics.Debug.WriteLine($"....... Got page code");
            PageData.Add(new KeyValuePair<string, string>("PageDataKey", pageKvpVal));

            return ExecutionResult.Next();
        }
    }
}
