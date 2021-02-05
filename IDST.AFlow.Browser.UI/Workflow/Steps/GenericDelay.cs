using IDST.AFlow.Browser.UI.Workflow.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace IDST.AFlow.Browser.UI.Workflow.Steps
{
    public class GenericDelay : StepBodyAsync
    {
        public int DelayMSec { get; set; }

        public WorkflowData workflowData { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            Task.Delay(DelayMSec).Wait();
            return ExecutionResult.Next();
        }
    }
}
