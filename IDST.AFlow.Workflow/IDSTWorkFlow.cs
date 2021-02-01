using IDST.AFlow.Workflow.Models;
using IDST.AFlow.Workflow.Steps;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;


namespace IDST.AFlow.Workflow
{
    public class IDSTWorkFlow : IWorkflow<WorkflowData>
    {
        private IntPtr _browserHandle { get; set; }

        public IDSTWorkFlow(IntPtr BrowserHandle)
        {
            _browserHandle = BrowserHandle;
        }

        public string Id { get { return ""; } }

        public int Version { get { return 1; } }

        public void Build(IWorkflowBuilder<WorkflowData> builder)
        {
            builder.StartWith(context =>
            {
                System.Diagnostics.Debug.WriteLine("Starting workflow....");
                return ExecutionResult.Next();
            })
            .Then<HtmlStepNavigate>()
                .Input(step => step.BrowserHandle, data => _browserHandle)
                .Input(step => step.NavigateUrl, data => "https://www.github.com")
                .Output(data => data.OutputData, step => step.PageData)
            .Then(context => {
                System.Diagnostics.Debug.WriteLine("Finishing workflow....");
                return ExecutionResult.Next();
            });

        }
    }
}
