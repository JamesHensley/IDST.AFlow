using IDST.AFlow.Browser.UI.Workflow.Models;
using IDST.AFlow.Browser.UI.Workflow.Steps;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;


namespace IDST.AFlow.Browser.UI.Workflow
{
    public class IDSTWorkFlow : IWorkflow<WorkflowData>
    {
        public string Id { get { return "IDSTWorkFlow"; } }

        public int Version { get { return 1; } }

        public WorkflowData workflowData;

        public void Build(IWorkflowBuilder<WorkflowData> builder)
        {

            builder.StartWith(context =>
            {
                System.Diagnostics.Debug.WriteLine("Starting workflow....");
                return ExecutionResult.Next();
            })
            .Then<HtmlStepNavigate>()
                .Input(step => step.BrowserHandle, data => workflowData.BrowserHandle)
                .Input(step => step.NavigateUrl, data => workflowData.NavigateUrl)
                .Input(step => step.PageData, data => new List<KeyValuePair<string, string>>())
                .Output(data => workflowData.OutputData, step => step.PageData)
            .Then(context => {
                System.Diagnostics.Debug.WriteLine("Finishing workflow....");
                return ExecutionResult.Next();
            });

        }
    }
}
