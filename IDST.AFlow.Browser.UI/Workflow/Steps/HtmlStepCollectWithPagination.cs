using CefSharp;
using IDST.AFlow.Browser.UI.Workflow.Models;
using IDST.AFlow.Browser.UI.WorkflowHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using Mapster;
using System.Dynamic;
using System.Linq;

namespace IDST.AFlow.Browser.UI.Workflow.Steps
{
    public class GitHubObj {
        public string txt { get; set; }

        public string link { get; set; }

        public string description { get; set; }

    }

    public class HtmlStepCollectWithPagination : StepBodyAsync
    {
        public WorkflowData workflowData { get; set; }

        /// <summary>
        /// Defines the CSS/Dom selector to use when locating the NEXT link
        /// </summary>
        public string NextElemSelector { get; set; }

        /// <summary>
        /// Limits the number of pages which should be scraped
        /// </summary>
        public int MaxPages { get; set; }

        /// <summary>
        /// Defines a javascript string to run for data gathering/scraping
        /// </summary>
        public string ScarapeJsCode { get; set; }

        /// <summary>
        /// Allows a delay between pagination requests to prevent killing the distant server
        /// </summary>
        public int PaginationDelay { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            int currentPage = 1;
            bool stopScrape = false;
            string currentUrl;
            List<object> outList = new List<object>();
            List<GitHubObj> GitHubObjList = new List<GitHubObj>();

            JavascriptResponse response;
            if(PaginationDelay == 0) { PaginationDelay = 100; }
            do
            {
                response = await BrowserMethods.ExecuteJS(workflowData.BrowserHandle, ScarapeJsCode);
                if (response.Success) {
                    outList.AddRange((List<object>)response.Result);
                } else {
                    stopScrape = true;
                }

                response = BrowserMethods.ExecuteJS(workflowData.BrowserHandle, NextElemSelector).Result;
                if (response.Success)
                {
                    currentUrl = response.Result.ToString();
                    currentPage++;
                    BrowserMethods.LoadPageAsync(workflowData.BrowserHandle, currentUrl).Wait();

                    Task.Delay(PaginationDelay).Wait();
                } else {
                    stopScrape = true;
                }
            }
            while (currentPage <= MaxPages && stopScrape == false);

            outList.ForEach(o => GitHubObjList.Add((o as ExpandoObject).Adapt<GitHubObj>()));

            workflowData.PersistentData.Add(new KeyValuePair<string, string>($"{context.Step.Id} - {context.Step.Name}", JsonSerializer.Serialize(GitHubObjList)));
            return ExecutionResult.Next();
        }
    }
}
