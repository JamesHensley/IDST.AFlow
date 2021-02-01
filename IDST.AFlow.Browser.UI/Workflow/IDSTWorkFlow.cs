﻿using IDST.AFlow.Browser.UI.Workflow.Models;
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

        //public WorkflowData workflowData = new WorkflowData();

        public void Build(IWorkflowBuilder<WorkflowData> builder)
        {
            builder
            .StartWith(context =>
            {
                System.Diagnostics.Debug.WriteLine("Starting workflow....");
                return ExecutionResult.Next();
            })
            .Then<AddNumbers>()
                .Input(step => step.Input1, data => 1)
                .Input(step => step.Input2, data => 2)
                .Output(data => data.TestInt, step => step.Output)
            .Then<HtmlStepNavigate>()
                .Input(step => step.NavigateUrl, data => "https://www.github.com")
                .Input(step => step.workflowData, data => data)
                .Output(data => data.PageData, step => step.workflowData.PageData)
            .Then<HtmlStepNavigate>()
                .Input(step => step.NavigateUrl, data => "https://www.hotmail.com")
                .Input(step => step.workflowData, data => data)
                .Output(data => data.PageData, step => step.workflowData.PageData)
            .Then<HtmlStepAnalyze>()
                .Input(step => step.workflowData, data => data)
                .Output(data => data.PageData, step => step.workflowData.PageData)
            .Then(context => {
                System.Diagnostics.Debug.WriteLine("Finishing workflow....");
                return ExecutionResult.Next();
            });
        }
    }
}
