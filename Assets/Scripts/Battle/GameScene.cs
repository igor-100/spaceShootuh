using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using SpaceShootuh.Core.Audio;
using SpaceShootuh.Core.Cameras;
using SpaceShootuh.Core.Controls;
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

        private void Awake()
        {
            gameCam = CompositionRoot.GetGameCamera();
            playerInput = CompositionRoot.GetPlayerInput();
            configuration = CompositionRoot.GetConfiguration();
            audioManager = CompositionRoot.GetAudioManager();
            sceneLoader = CompositionRoot.GetSceneLoader();
            gameplay = CompositionRoot.GetGameplay();

            var uiRoot = CompositionRoot.GetUIRoot();
        }
    }
}
