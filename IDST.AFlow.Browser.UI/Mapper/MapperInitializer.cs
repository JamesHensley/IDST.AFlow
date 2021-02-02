using IDST.AFlow.Browser.UI.Workflow.Steps;
using Mapster;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace IDST.AFlow.Browser.UI.Mapper
{
    public class MapperInitializer : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ExpandoObject, GitHubObj>();
            config.NewConfig<object, GitHubObj>();
        }
    }
}
