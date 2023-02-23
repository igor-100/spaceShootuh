using SpaceShootuh.Core;
using System;
using UnityEngine;

namespace SpaceShootuh.UI.GameOver
{
    public class GameOverScreen : MonoBehaviour, IGameOverScreen
    {
        private IGameOverView View;
        private ISceneLoader SceneLoader;

        private void Awake()
        {
            SceneLoader = CompositionRoot.GetSceneLoader();
            var viewFactory = CompositionRoot.GetViewFactory();

            View = viewFactory.CreateGameOverScreen();

            View.RestartClicked += OnRestartClicked;
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
            Time.timeScale = 0f;
        }

        private static void ToNormalSpeed()
        {
            Time.timeScale = 1f;
        }

        public void SetScore(int score)
        {
            View.SetScore(score);
        }
    }
}
