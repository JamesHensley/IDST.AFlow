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
    public class HtmlStepExecuteJS : StepBodyAsync
    {
        public WorkflowData workflowData { get; set; }

        public string ScriptCode { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            System.Diagnostics.Debug.WriteLine($"Running {ScriptCode}");

            var scriptResult = BrowserMethods.ExecuteJSAsync(workflowData.BrowserHandle, ScriptCode).Result;
            workflowData.PageData.Add(new KeyValuePair<string, string>($"{context.Step.Id} {scriptResult}", scriptResult));
            return ExecutionResult.Next();
        }
    }
}
