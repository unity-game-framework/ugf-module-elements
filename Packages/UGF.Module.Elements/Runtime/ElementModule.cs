using System;
using UGF.Application.Runtime;
using UGF.Elements.Runtime;

namespace UGF.Module.Elements.Runtime
{
    public class ElementModule : ApplicationModuleBase, IElementModule, IApplicationLauncherEventHandler
    {
        public IElementContext Context { get; }
        public IElementCollection Elements { get { return m_parent.Children; } }

        private readonly ElementParent<IElement> m_parent = new ElementParent<IElement>();

        public ElementModule(IElementContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            m_parent.Initialize();
        }

        protected override void OnPreUninitialize()
        {
            base.OnPreUninitialize();

            m_parent.Uninitialize();
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
