using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using SpaceShootuh.Core.Audio;
using SpaceShootuh.Core.Cameras;
using SpaceShootuh.Core.Controls;
using UnityEngine;

namespace SpaceShootuh.Gameplay
{
    public class GameScene : MonoBehaviour
    {
        private IGameCamera GameCam;
        private IPlayerInput PlayerInput;
        private IConfiguration Configuration;
        private IAudioManager AudioManager;
        private ISceneLoader SceneLoader;

        private void Awake()
        {
            GameCam = CompositionRoot.GetGameCamera();
            PlayerInput = CompositionRoot.GetPlayerInput();
            Configuration = CompositionRoot.GetConfiguration();
            AudioManager = CompositionRoot.GetAudioManager();
            SceneLoader = CompositionRoot.GetSceneLoader();

            var uiRoot = CompositionRoot.GetUIRoot();
        }
    }
}
