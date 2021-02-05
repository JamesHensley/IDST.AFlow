using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;

namespace IDST.AFlow.Browser.UI.Workflow
{
    public class WorkflowExpando {
    }

    public static class WorkflowEngine
    {
        private static List<KeyValuePair<string, ExpandoObject>> _modelList;

        private static void LoadModels() {
            var files = Directory.GetFiles(Path.GetFullPath(@".\Workflow\ServiceModels"), "*.json").ToList();
            _modelList = new List<KeyValuePair<string, ExpandoObject>>();

            files.ForEach(o => {
                try {
                    var model = JsonSerializer.Deserialize<ExpandoObject>(File.ReadAllText(o));
                    if (model.ToList().Any(o => o.Key == "IDSTWorkFlowModel")) {
                        var eo = new ExpandoObject();
                        string entryName = "";

                        foreach (KeyValuePair<string, object> kvp in model)
                        {
                            if (kvp.Key != "IDSTWorkFlowModel") {
                                eo.TryAdd(kvp.Key, kvp.Value);
                            }
                            else
                            {
                                entryName = kvp.Value.ToString();
                            }
                        }

                        _modelList.Add(new KeyValuePair<string, ExpandoObject>(entryName, eo));
                    }
                }
                catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine($"Could not Deserialize '{o}' as a valid Client Model. {ex.Message}");
                }
            });
        }

        public static ExpandoObject GetClientModel(string nameStr) {
            if (_modelList == null)
            {
                LoadModels();
            }

            var model = _modelList.FirstOrDefault(o => o.Key == nameStr);
            return null;
        }
    }
}
