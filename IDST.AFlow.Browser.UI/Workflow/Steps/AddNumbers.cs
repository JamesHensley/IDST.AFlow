using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace IDST.AFlow.Browser.UI.Workflow.Steps
{
    public class AddNumbers : StepBodyAsync
    {
        public int Input1 { get; set; }
        public int Input2 { get; set; }
        public int Output { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            System.Diagnostics.Debug.WriteLine($"Running Step: AddNumbers {Input1} + {Input2}");
            Output = (Input1 + Input2);
            return ExecutionResult.Next();
        }
    }
}
