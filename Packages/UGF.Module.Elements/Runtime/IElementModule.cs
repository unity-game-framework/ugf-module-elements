using UGF.Application.Runtime;
using UGF.Elements.Runtime;

namespace UGF.Module.Elements.Runtime
{
    public interface IElementModule : IApplicationModule
    {
        IElementContext Context { get; }
    }
}
