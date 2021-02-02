using CefSharp;
using IDST.AFlow.Browser.UI.Workflow.Models;
using IDST.AFlow.Browser.UI.WorkflowHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using System.Text.Json;

namespace IDST.AFlow.Browser.UI.Workflow.Steps
{
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
            List<string> outList = new List<string>();

            JavascriptResponse response;
            if(PaginationDelay == 0) { PaginationDelay = 100; }

            do
            {
                response = BrowserMethods.ExecuteJSAsync(workflowData.BrowserHandle, ScarapeJsCode).Result;
                if (response.Success) {
                    outList.Add(JsonSerializer.Serialize(response.Result));
                } else {
                    stopScrape = true;
                }

                response = BrowserMethods.ExecuteJSAsync(workflowData.BrowserHandle, NextElemSelector).Result;
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

            workflowData.PersistentData.Add(new KeyValuePair<string, string>($"{context.Step.Id} - {context.Step.Name}", JsonSerializer.Serialize(outList)));
            return ExecutionResult.Next();
        }
    }
}
