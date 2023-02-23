using SpaceShootuh.UI.GameHUD;

namespace SpaceShootuh.UI
{
    public interface IViewFactory
    {
        IGameHUDView CreateGameHUD();
    }
}
