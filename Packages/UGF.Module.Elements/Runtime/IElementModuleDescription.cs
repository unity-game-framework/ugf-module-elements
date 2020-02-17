using System.Collections.Generic;
using UGF.Elements.Runtime;

namespace UGF.Module.Elements.Runtime
{
    public interface IElementModuleDescription
    {
        IReadOnlyList<IElementBuilder> Elements { get; }
    }
}
