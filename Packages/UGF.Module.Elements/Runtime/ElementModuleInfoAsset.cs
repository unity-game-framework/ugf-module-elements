using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Elements.Runtime;
using UnityEngine;

namespace UGF.Module.Elements.Runtime
{
    [CreateAssetMenu(menuName = "UGF/Module.Elements/ElementModuleInfo", order = 2000)]
    public class ElementModuleInfoAsset : ApplicationModuleInfoAsset<IElementModule>
    {
        [SerializeField] private List<ElementBuilderAsset> m_elements = new List<ElementBuilderAsset>();

        public List<ElementBuilderAsset> Elements { get { return m_elements; } }

        public IElementModuleDescription GetDescription()
        {
            return new ElementModuleDescription
            {
                Elements = new List<IElementBuilder>(m_elements)
            };
        }

        protected override IApplicationModule OnBuild(IApplication application)
        {
            var context = new ElementContext { application };
            IElementModuleDescription description = GetDescription();

            return new ElementModule(context, description);
        }
    }
}
