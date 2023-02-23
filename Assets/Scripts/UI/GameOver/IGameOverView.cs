using System;

namespace SpaceShootuh.UI.GameOver
{
    public interface IGameOverView : IView
    {
        public event Action RestartClicked;
        void SetScore(int score);
    }
}
