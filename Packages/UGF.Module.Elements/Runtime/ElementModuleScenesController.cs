#if UGF_MODULE_ELEMENTS_SCENES
using System;
using System.Collections.Generic;
using UGF.Application.Runtime;
using UGF.Elements.Runtime;
using UGF.Initialize.Runtime;
using UGF.Module.Scenes.Runtime;
using UnityEngine.SceneManagement;

namespace UGF.Module.Elements.Runtime
{
    internal class ElementModuleScenesController : InitializeBase
    {
        public IElementContext Context { get; }
        public ISceneModule SceneModule { get; }

        public ElementModuleScenesController(IElementContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            SceneModule = Context.Get<IApplication>().GetModule<ISceneModule>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach (KeyValuePair<Scene, SceneController> pair in SceneModule.Controllers)
            {
                CreateElements(pair.Value, true);
            }

            SceneModule.ControllerAdd += OnSceneModuleControllerAdd;
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            SceneModule.ControllerAdd -= OnSceneModuleControllerAdd;
        }

        private void OnSceneModuleControllerAdd(SceneController controller)
        {
            CreateElements(controller, false);
        }

        private void CreateElements(SceneController controller, bool initialize)
        {
            if (controller.HasContainer)
            {
                SceneContainer container = controller.Container;

                for (int i = 0; i < container.Containers.Count; i++)
                {
                    if (container.Containers[i] is IElementBuilder builder)
                    {
                        IElement element = builder.Build(Context);

                        controller.Children.Add(element);

                        if (initialize)
                        {
                            element.Initialize();
                        }
                    }
                }
            }
        }
    }
}
#endif
