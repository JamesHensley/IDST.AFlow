﻿using IDST.AFlow.Browser.UI.Workflow.Models;
using IDST.AFlow.Browser.UI.Workflow.Steps;
using System.Collections.Generic;
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
                    Array.from(document.querySelectorAll('div.codesearch-results ul.repo-list > li.repo-list-item'))
                    .map(d => {
                        return {
                            txt: (d.querySelector('a[data-hydro-click]') ? d.querySelector('a[data-hydro-click]').innerText : ''),
                            link: (d.querySelector('a[data-hydro-click]') ? d.querySelector('a[data-hydro-click]').href : ''),
                            description: (d.querySelector('p') ? d.querySelector('p').innerText : '')
                        }
                    });
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
                .Output(data => data.PersistentData, step => step.workflowData.PersistentData)
            .Then<HtmlStepExecuteJS>()              //Populate the SEARCH BOX and execute search
                .Input(step => step.ScriptCode, data => scripts.Find(o => o.Key == 1).Value)
                .Input(step => step.workflowData, data => data)
                .Output(data => data.PersistentData, step => step.workflowData.PersistentData)
            .Then<GenericDelay>()                   //Wait for 2 seconds...this is only temporary
                .Input(step => step.DelayMSec, data => 2000)
                .Input(step => step.workflowData, data => data)
                .Output(data => data.PersistentData, step => step.workflowData.PersistentData)
            .Then<HtmlStepCollectWithPagination>()  //Start scraping the results page and navigating to the next set of results
                .Input(step => step.workflowData, data => data)
                .Input(step => step.MaxPages, data => 1)
                .Input(step => step.NextElemSelector, data => @"document.querySelector('div.paginate-container div[role=""navigation""] a.next_page').href")
                .Input(step => step.ScarapeJsCode, data => scripts.Find(o => o.Key == 2).Value)
                .Input(step => step.PaginationDelay, data => 500)
                .Input(step => step.OutputToFinal, data => true)
                .Output(data => data.PersistentData, step => step.workflowData.PersistentData)
            .Then<HtmlStepNavigate>()
                .Input(step => step.NavigateUrl, data => "https://www.hotmail.com")
                .Input(step => step.workflowData, data => data)
                .Output(data => data.PersistentData, step => step.workflowData.PersistentData)
            .Then<OutputStepCSV>()                  // Write all exported data to a text file
                .Input(step => step.workflowData, data => data)
                .Input(step => step.ExportLocation, data => @"c:\Temp\OutputFile.json")
                .Output(data => data.PersistentData, step => step.workflowData.PersistentData)
            .Then(context => {
                System.Diagnostics.Debug.WriteLine($"Finishing workflow... Step: {context.Step.Id}");
                return ExecutionResult.Next();
            });
        }
    }
}
