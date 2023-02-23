using SpaceShootuh.UI.GameHUD;
using SpaceShootuh.UI.GameOver;
using SpaceShootuh.UI.Pause;

namespace SpaceShootuh.UI
{
    public interface IViewFactory
    {
        IGameHUDView CreateGameHUD();
        IGameOverView CreateGameOverScreen();
        IPauseScreenView CreatePauseScreen();
    }
}
