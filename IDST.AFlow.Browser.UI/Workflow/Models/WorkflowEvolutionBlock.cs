using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDST.AFlow.Browser.UI.Workflow.Models
{
    public enum WorkflowBlockStages
    {
        Navigate = 1,
        Execute = 2,
        Wait = 3,
        Process = 4
    }

    /// <summary>
    /// Represents an entire process (block) in the workflow
    /// </summary>
    public abstract class WorkflowEvolutionBlock
    {
        public string BlockName { get; set; }

        /// <summary>
        /// Collection of key/value pairs feeding this block
        /// </summary>
        public List<KeyValuePair<string, string>> InputData { get; set; }

        /// <summary>
        /// Collection of key/value pairs resulting from this block
        /// </summary>
        public List<KeyValuePair<string, string>> OutputData { get; set; }

        public abstract Task<string> Navigate();
        
        public abstract Task<bool> Run();

        public abstract Task<bool> Wait();

        public abstract Task<List<KeyValuePair<string, string>>> Process();
    }
}
