using SpaceShootuh.Core;
using UnityEngine;

namespace SpaceShootuh.UI.GameHUD
{
    public class GameHUD : MonoBehaviour, IGameHUD
    {
        private IGameHUDView View;

        private void Awake()
        {
            var viewFactory = CompositionRoot.GetViewFactory();
            View = viewFactory.CreateGameHUD();

            SetScore(0);
        }

        public void Show()
        {
            View.Show();
        }

        public void Hide()
        {
            View.Hide();
        }

        public void SetHealth(float value)
        {
            View.SetHP(value);
        }

        public void SetScore(int value)
        {
            View.SetScore(value);
        }
    }
}