using System;
using UGF.Application.Runtime;
using UGF.Elements.Runtime;
using UGF.Initialize.Runtime;

namespace UGF.Module.Elements.Runtime
{
    public class ElementModule : ApplicationModuleBase, IElementModule, IApplicationLauncherEventHandler
    {
        public IElementContext Context { get; }
        public IElementModuleDescription Description { get; }

        private readonly InitializeCollection m_elements = new InitializeCollection();

        public ElementModule(IElementContext context, IElementModuleDescription description)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Description = description ?? throw new ArgumentNullException(nameof(description));

            for (int i = 0; i < Description.Elements.Count; i++)
            {
                IElementBuilder builder = Description.Elements[i];
                IElement element = builder.Build(Context);

                m_elements.Add(element);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_elements.Initialize();
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            m_elements.Uninitialize();
        }

        void IApplicationLauncherEventHandler.OnLaunched(IApplication application)
        {
        }

        void IApplicationLauncherEventHandler.OnStopped(IApplication application)
        {
        }

        void IApplicationLauncherEventHandler.OnQuitting(IApplication application)
        {
        }
    }
}
