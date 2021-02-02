using IDST.AFlow.Browser.UI.Workflow.Models;
using IDST.AFlow.Browser.UI.WorkflowHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace IDST.AFlow.Browser.UI.Workflow.Steps
{
    public class HtmlStepNavigate : StepBodyAsync
    {
        public WorkflowData workflowData { get; set; }

        public string NavigateUrl { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            System.Diagnostics.Debug.WriteLine($"RunAsync Step: {context.Step.Id} - {NavigateUrl}");
            var pageSourceHtml = BrowserMethods.LoadPageAsync(workflowData.BrowserHandle, NavigateUrl).Result;

            workflowData.PageData.Add(new KeyValuePair<string, string>($"{NavigateUrl}", pageSourceHtml));
            return ExecutionResult.Next();
        }
    }
}
