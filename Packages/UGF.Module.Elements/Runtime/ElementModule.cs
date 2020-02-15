using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Elements.Runtime;
using UGF.Initialize.Runtime;

namespace UGF.Module.Elements.Runtime
{
    public class ElementModule : ApplicationModuleBase, IElementModule, IApplicationLauncherEventHandler
    {
        public IElementContext Context { get; }
        public IElementModuleDescription Description { get; }
        public IReadOnlyList<IElement> Elements { get { return m_elements.Collection; } }

        private readonly InitializeCollection<IElement> m_elements = new InitializeCollection<IElement>();

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

        public T Get<T>() where T : IElement
        {
            return (T)Get(typeof(T));
        }

        public IElement Get(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!TryGet(type, out IElement element))
            {
                throw new ArgumentException($"Element not found by the specified type: '{type}'.");
            }

            return element;
        }

        public bool TryGet<T>(out T element) where T : IElement
        {
            if (TryGet(typeof(T), out IElement value))
            {
                element = (T)value;
                return true;
            }

            element = default;
            return false;
        }

        public bool TryGet(Type type, out IElement element)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            for (int i = 0; i < m_elements.Collection.Count; i++)
            {
                element = m_elements.Collection[i];

                if (type.IsInstanceOfType(element))
                {
                    return true;
                }
            }

            element = default;
            return false;
        }

        void IApplicationLauncherEventHandler.OnLaunched(IApplication application)
        {
            for (int i = 0; i < m_elements.Collection.Count; i++)
            {
                if (m_elements.Collection[i] is IApplicationLauncherEventHandler handler)
                {
                    handler.OnLaunched(application);
                }
            }
        }

        void IApplicationLauncherEventHandler.OnStopped(IApplication application)
        {
            for (int i = 0; i < m_elements.Collection.Count; i++)
            {
                if (m_elements.Collection[i] is IApplicationLauncherEventHandler handler)
                {
                    handler.OnStopped(application);
                }
            }
        }

        void IApplicationLauncherEventHandler.OnQuitting(IApplication application)
        {
            for (int i = 0; i < m_elements.Collection.Count; i++)
            {
                if (m_elements.Collection[i] is IApplicationLauncherEventHandler handler)
                {
                    handler.OnQuitting(application);
                }
            }
        }
    }
}
