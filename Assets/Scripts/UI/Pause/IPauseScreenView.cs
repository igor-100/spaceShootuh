using System;
using System.Collections.Generic;

namespace SpaceShootuh.UI.Pause
{
    public interface IPauseScreenView : IView
    {
        public event Action ResumeClicked;
        public event Action RestartClicked;
    }
}
