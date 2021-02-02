using IDST.AFlow.Browser.UI.Workflow.Models;
using IDST.AFlow.Browser.UI.WorkflowHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace IDST.AFlow.Browser.UI.Workflow.Steps
{
    public class OutputStepCSV : StepBodyAsync
    {
        public WorkflowData workflowData { get; set; }

        public string ExportLocation { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {

            var outStr = JsonSerializer.Serialize(workflowData, new JsonSerializerOptions() { WriteIndented = true });

            TextWriter writer = new StreamWriter(ExportLocation, false);
            writer.Write(outStr);
            writer.Flush();
            writer.Close();

            workflowData.PersistentData.Add(new KeyValuePair<string, string>($"{context.Step.Id} {context.Step.Name}", outStr));
            return ExecutionResult.Next();
        }
    }
}
