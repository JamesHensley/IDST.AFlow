using System;
using System.Collections.Generic;
using System.Text;

namespace IDST.AFlow.Browser.UI.Mapper
{
    internal interface IResolve<TInput, TOutput>
    {
        TOutput Resolve(TInput input);
    }
}