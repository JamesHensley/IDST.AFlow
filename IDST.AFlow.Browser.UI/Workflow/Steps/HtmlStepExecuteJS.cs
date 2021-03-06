﻿using IDST.AFlow.Browser.UI.Workflow.Models;
using IDST.AFlow.Browser.UI.WorkflowHelpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
            var scriptResult = BrowserMethods.ExecuteJS(workflowData.BrowserHandle, ScriptCode).Result;
            if (scriptResult.Success) {
                workflowData.PersistentData.ScriptResult = (ExpandoObject)scriptResult.Result;
            }
            return ExecutionResult.Next();
        }
    }
}
