using SpaceShootuh.Core;
using System;
using UnityEngine;

namespace SpaceShootuh.UI.Pause
{
    public class PauseScreen : MonoBehaviour, IPauseScreen
    {
        // TODO: Extract to some Game State Service
        private bool gameIsPaused = false;

        private IPauseScreenView View;
        private ISceneLoader SceneLoader;

        public event Action Paused = () => { };

        private void Awake()
        {
            SceneLoader = CompositionRoot.GetSceneLoader();
            var playerInput = CompositionRoot.GetPlayerInput();
            var viewFactory = CompositionRoot.GetViewFactory();

            playerInput.Escape += OnEscape;

            View = viewFactory.CreatePauseScreen();

            View.ResumeClicked += OnResumeClicked;
            View.RestartClicked += OnRestartClicked;
        }

        private void OnEscape()
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        private void OnResumeClicked()
        {
            Resume();
        }

        private void OnRestartClicked()
        {
            ToNormalSpeed();
            SceneLoader.RestartScene();
        }

        public void Hide()
        {
            View.Hide();
        }

        public void Show()
        {
            View.Show();
        }

        private void Pause()
        {
            Show();
            Time.timeScale = 0f;
            gameIsPaused = true;
            Paused();
        }

        private void Resume()
        {
            Hide();
            ToNormalSpeed();
            gameIsPaused = false;
        }

        private static void ToNormalSpeed()
        {
            Time.timeScale = 1f;
        }
    }
}
