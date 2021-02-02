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
            var scriptResult = BrowserMethods.ExecuteJSAsync(workflowData.BrowserHandle, ScriptCode).Result;
            if (scriptResult.Success) {
                workflowData.PersistentData.Add(new KeyValuePair<string, string>($"{context.Step.Id} {context.Step.Name}", scriptResult.Result?.ToString() ?? "No Result"));
            }
            return ExecutionResult.Next();
        }
    }
}
