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
    public class HtmlStepAnalyze : StepBodyAsync
    {
        public WorkflowData workflowData { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            workflowData.PersistentData.PageData.ForEach(o => {
                System.Diagnostics.Debug.WriteLine($"Step: {context.Step.Id} {o.Key} {o.Value.Length}");
            });
            return ExecutionResult.Next();
        }
    }
}
