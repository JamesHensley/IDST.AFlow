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
            config.NewConfig<ExpandoObject, GitHubObj>();
            config.NewConfig<object, GitHubObj>();
        }
    }

    public class Mapper : Mapster.c MapsterMapper.Mapper {
        /*
        public override void Initialise()
        {
            TypeAdapterConfig<Foo, Foo>.NewConfig()
                .Map(dest => dest.Foos, src => src.Foos ?? new List<Foo>())
                .Map(dest => dest.FooArray, src => src.FooArray ?? new Foo[0])
                .Map(dest => dest.Ints, src => src.Ints ?? Enumerable.Empty<int>())
                .Map(dest => dest.IntArray, src => src.IntArray ?? new int[0])
                .Compile();
        }
        */
    }
}
