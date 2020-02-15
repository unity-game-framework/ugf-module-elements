using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Elements.Runtime;

namespace UGF.Module.Elements.Runtime
{
    public interface IElementModule : IApplicationModule
    {
        IElementContext Context { get; }
        IElementModuleDescription Description { get; }
        IReadOnlyList<IElement> Elements { get; }

        T Get<T>() where T : IElement;
        IElement Get(Type type);
        bool TryGet<T>(out T element) where T : IElement;
        bool TryGet(Type type, out IElement element);
    }
}
