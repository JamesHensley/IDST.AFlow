using IDST.AFlow.Browser.UI.Mapper.Resolvers;
using IDST.AFlow.Browser.UI.Workflow.Steps;
using Mapster;
using MapsterMapper;
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
            /*
            config.NewConfig<ExpandoObject, GitHubObj>()
                .Map(destinationMember => destinationMember.description, srcMember => new ResolveExpandoField().Resolve(new Tuple<ExpandoObject, string>(srcMember, "description")))
                .Map(destinationMember => destinationMember.link, srcMember => new ResolveExpandoField().Resolve(new Tuple<ExpandoObject, string>(srcMember, "link")))
                .Map(destinationMember => destinationMember.txt, srcMember => new ResolveExpandoField().Resolve(new Tuple<ExpandoObject, string>(srcMember, "txt")));
            */
        }
    }

}
