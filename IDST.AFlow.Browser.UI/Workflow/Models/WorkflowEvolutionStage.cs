using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDST.AFlow.Browser.UI.Workflow.Models
{
    /// <summary>
    /// Represents a specific stage in a workflow
    /// </summary>
    public abstract class WorkflowEvolutionStage
    {
        private List<KeyValuePair<string, string>> _ingestData;

        /// <summary>
        /// 
        /// </summary>
        public string StageName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public WorkflowEvolutionBlock Block { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IngestData"></param>
        public void Ingest(List<KeyValuePair<string, string>> IngestData) {
            _ingestData = IngestData;
        }

        //Do something and return the new data
        public abstract Task<WorkflowEvolutionStage> ProcessStage();
    }
}
