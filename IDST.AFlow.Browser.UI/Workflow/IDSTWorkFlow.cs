using IDST.AFlow.Browser.UI.Workflow.Models;
using IDST.AFlow.Browser.UI.Workflow.Steps;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;


namespace IDST.AFlow.Browser.UI.Workflow
{
    public class IDSTWorkFlow : IWorkflow<WorkflowData>
    {
        public string Id { get { return "IDSTWorkFlow"; } }

        public int Version { get { return 1; } }

        public void Build(IWorkflowBuilder<WorkflowData> builder)
        {
            var scripts = new List<KeyValuePair<int, string>>() {
                new KeyValuePair<int, string>(1, @"
                    var domE = document.querySelector(""input[type='text'].header-search-input"");
                    domE.value = 'cefsharp';
                    document.querySelector(""form[role='search'].js-site-search-form"").submit();
                "),
                new KeyValuePair<int, string>(2, @"
                    Array.from(document.querySelectorAll(""div.codesearch-results ul.repo-list > li.repo-list-item p.mb-1 "")).map(d => d.innerText);
                ")
            };

            builder
            .StartWith(context =>
            {
                System.Diagnostics.Debug.WriteLine($"Starting workflow... Step: {context.Step.Id}");
                return ExecutionResult.Next();
            })
            .Then<HtmlStepNavigate>()
                .Input(step => step.NavigateUrl, data => "https://www.github.com")
                .Input(step => step.workflowData, data => data)
                .Output(data => data.PageData, step => step.workflowData.PageData)
            .Then<HtmlStepExecuteJS>()
                .Input(step => step.ScriptCode, data => scripts.Find(o => o.Key == 1).Value)
                .Input(step => step.workflowData, data => data)
                .Output(data => data.PageData, step => step.workflowData.PageData)
            .Then(context => {
                Task.Delay(2000).Wait();
                return ExecutionResult.Next();
            })
            .Then<HtmlStepExecuteJS>()
                .Input(step => step.ScriptCode, data => scripts.Find(o => o.Key == 2).Value)
                .Input(step => step.workflowData, data => data)
                .Output(data => data.PageData, step => step.workflowData.PageData)
            .Then<HtmlStepAnalyze>()
                .Input(step => step.workflowData, data => data)
                .Output(data => data.PageData, step => step.workflowData.PageData)
            .Then(context => {
                System.Diagnostics.Debug.WriteLine($"Finishing workflow... Step: {context.Step.Id}");
                return ExecutionResult.Next();
            });
        }
    }
}
