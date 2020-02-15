using UGF.Application.Runtime;
using UGF.Elements.Runtime;

namespace UGF.Module.Elements.Runtime
{
    public class ElementModuleInfoAsset : ApplicationModuleInfoAsset<IElementModule>
    {
        public IElementModuleDescription GetDescription()
        {
            return null;
        }

        protected override IApplicationModule OnBuild(IApplication application)
        {
            var context = new ElementContext { application };
            IElementModuleDescription description = GetDescription();

            return new ElementModule(context, description);
        }
    }
}
