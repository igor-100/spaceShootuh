using System;

namespace SpaceShootuh.UI.GameOver
{
    public interface IGameOverScreen : IScreen
    {
        void SetScore(int score);
    }
}
