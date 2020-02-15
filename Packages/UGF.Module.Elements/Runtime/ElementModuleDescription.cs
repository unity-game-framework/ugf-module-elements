using System.Collections.Generic;
using UGF.Elements.Runtime;

namespace UGF.Module.Elements.Runtime
{
    public class ElementModuleDescription : IElementModuleDescription
    {
        public List<IElementBuilder> Elements { get; set; } = new List<IElementBuilder>();

        IReadOnlyList<IElementBuilder> IElementModuleDescription.Elements { get { return Elements; } }
    }
}
