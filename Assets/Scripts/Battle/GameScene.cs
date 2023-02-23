using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using SpaceShootuh.Core.Audio;
using SpaceShootuh.Core.Cameras;
using SpaceShootuh.Core.Controls;
using SpaceShootuh.UI.GameOver;
using SpaceShootuh.UI.Pause;
using UnityEngine;

namespace SpaceShootuh.Battle
{
    public class GameScene : MonoBehaviour
    {
        private IGameCamera gameCam;
        private IPlayerInput playerInput;
        private IConfiguration configuration;
        private IAudioManager audioManager;
        private ISceneLoader sceneLoader;
        private IGameplay gameplay;
        private IPauseScreen pauseScreen;
        private IGameOverScreen gameOverScreen;

        private void Awake()
        {
            gameCam = CompositionRoot.GetGameCamera();
            playerInput = CompositionRoot.GetPlayerInput();
            configuration = CompositionRoot.GetConfiguration();
            audioManager = CompositionRoot.GetAudioManager();
            sceneLoader = CompositionRoot.GetSceneLoader();

            gameplay = CompositionRoot.GetGameplay();
            gameplay.SetLevelProperties(configuration.GetLevel());

            var uiRoot = CompositionRoot.GetUIRoot();
            pauseScreen = CompositionRoot.GetPauseScreen();
            gameOverScreen = CompositionRoot.GetGameOverScreen();

            gameplay.GameOver += OnGameOver;
        }

        private void Start()
        {
            pauseScreen.Hide();
            gameOverScreen.Hide();
        }

        private void OnGameOver(int score)
        {
            gameOverScreen.Show();
            gameOverScreen.SetScore(score);
            gameplay.GameOver -= OnGameOver;
        }
    }
}
