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
            var module = new ElementModule(context, description);

            for (int i = 0; i < m_elements.Count; i++)
            {
                ElementBuilderAsset builder = m_elements[i];
                IElement element = builder.Build(context);

                module.Elements.Add(element);
            }

            return module;
        }
    }
}
