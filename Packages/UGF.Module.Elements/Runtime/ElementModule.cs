using System;
using UGF.Application.Runtime;
using UGF.Elements.Runtime;

namespace UGF.Module.Elements.Runtime
{
    public class ElementModule : ApplicationModuleBase, IElementModule, IApplicationLauncherEventHandler
    {
        public IElementContext Context { get; }
        public IElementModuleDescription Description { get; }
        public IElementCollection Elements { get { return m_parent.Children; } }

        private readonly ElementParent<IElement> m_parent = new ElementParent<IElement>();

        public ElementModule(IElementContext context, IElementModuleDescription description)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            for (int i = 0; i < Description.Elements.Count; i++)
            {
                IElementBuilder builder = Description.Elements[i];
                IElement element = builder.Build(Context);

                m_parent.Children.Add(element);
            }

            m_parent.Initialize();
        }

        protected override void OnPreUninitialize()
        {
            base.OnPreUninitialize();

            m_parent.Uninitialize();
            m_parent.Children.Clear();
        }

        void IApplicationLauncherEventHandler.OnLaunched(IApplication application)
        {
            for (int i = 0; i < m_parent.Children.Count; i++)
            {
                if (m_parent.Children[i] is IApplicationLauncherEventHandler handler)
                {
                    handler.OnLaunched(application);
                }
            }
        }

        void IApplicationLauncherEventHandler.OnStopped(IApplication application)
        {
            for (int i = 0; i < m_parent.Children.Count; i++)
            {
                if (m_parent.Children[i] is IApplicationLauncherEventHandler handler)
                {
                    handler.OnStopped(application);
                }
            }
        }

        void IApplicationLauncherEventHandler.OnQuitting(IApplication application)
        {
            for (int i = 0; i < m_parent.Children.Count; i++)
            {
                if (m_parent.Children[i] is IApplicationLauncherEventHandler handler)
                {
                    handler.OnQuitting(application);
                }
            }
        }
    }
}
