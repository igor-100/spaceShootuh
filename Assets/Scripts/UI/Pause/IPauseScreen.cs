using System;

namespace SpaceShootuh.UI.Pause
{
    public interface IPauseScreen : IScreen
    {
        event Action Paused;
    }
}
