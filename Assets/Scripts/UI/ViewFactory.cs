using SpaceShootuh.Core;
using SpaceShootuh.UI.GameHUD;
using SpaceShootuh.UI.GameOver;
using SpaceShootuh.UI.Pause;

namespace SpaceShootuh.UI
{
    public class ViewFactory : IViewFactory
    {
        private readonly IUIRoot uiRoot;
        private readonly IResourceManager resourceManager;

        public ViewFactory(IUIRoot uiRoot, IResourceManager resourceManager)
        {
            this.uiRoot = uiRoot;
            this.resourceManager = resourceManager;
        }

        public IGameHUDView CreateGameHUD()
        {
            var view = resourceManager.CreatePrefabInstance<IGameHUDView, EViews>(EViews.GameHUD);
            view.SetParent(uiRoot.MainCanvas);

            return view;
        }

        public IPauseScreenView CreatePauseScreen()
        {
            var view = resourceManager.CreatePrefabInstance<IPauseScreenView, EViews>(EViews.Pause);
            view.SetParent(uiRoot.OverlayCanvas);

            return view;
        }

        public IGameOverView CreateGameOverScreen()
        {
            var view = resourceManager.CreatePrefabInstance<IGameOverView, EViews>(EViews.GameOver);
            view.SetParent(uiRoot.OverlayCanvas);

            return view;
        }
    }
}
