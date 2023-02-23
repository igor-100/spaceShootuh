using SpaceShootuh.Core;
using SpaceShootuh.UI.GameHUD;

namespace SpaceShootuh.UI
{
    public class ViewFactory : IViewFactory
    {
        private IUIRoot UIRoot;
        private IResourceManager ResourceManager;

        public ViewFactory(IUIRoot uiRoot, IResourceManager resourceManager)
        {
            UIRoot = uiRoot;
            ResourceManager = resourceManager;
        }

        public IGameHUDView CreateGameHUD()
        {
            var view = ResourceManager.CreatePrefabInstance<IGameHUDView, EViews>(EViews.GameHUD);
            view.SetParent(UIRoot.MainCanvas);

            return view;
        }
    }
}
