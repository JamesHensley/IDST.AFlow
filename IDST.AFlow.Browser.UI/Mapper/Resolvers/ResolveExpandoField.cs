using IDST.AFlow.Browser.UI.Workflow.Steps;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace IDST.AFlow.Browser.UI.Mapper.Resolvers
{
    public class ResolveExpandoField : IResolve<Tuple<ExpandoObject, string>, string>
    {
        public string Resolve(Tuple<ExpandoObject, string> input)
        {
            object retVal;
            IDictionary<string, object> dictionary_object = input.Item1;
            dictionary_object.TryGetValue(input.Item2, out retVal);

            return retVal?.ToString() ?? "";
        }
    }
}