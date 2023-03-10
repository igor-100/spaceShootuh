using SpaceShootuh.Battle;
using SpaceShootuh.Battle.Environment;
using SpaceShootuh.Battle.Units;
using SpaceShootuh.Configurations;
using SpaceShootuh.Core.Audio;
using SpaceShootuh.Core.Cameras;
using SpaceShootuh.Core.Controls;
using SpaceShootuh.UI;
using SpaceShootuh.UI.GameHUD;
using SpaceShootuh.UI.GameOver;
using SpaceShootuh.UI.Pause;
using System;
using UnityEngine;

namespace SpaceShootuh.Core
{
    public class CompositionRoot : MonoBehaviour
    {
        private static IUIRoot UIRoot;
        private static IPlayerInput PlayerInput;
        private static IGameCamera GameCamera;
        private static IViewFactory ViewFactory;
        private static ISceneLoader SceneLoader;
        private static IResourceManager ResourceManager;
        private static IAudioManager AudioManager;
        private static IConfiguration Configuration;
        private static IGameplay Gameplay;
        private static IPlayer Player;
        private static ILevel Level;
        private static IGameHUD GameHUD;
        private static IPauseScreen PauseScreen;
        private static IGameOverScreen GameOverScreen;

        private void OnDestroy()
        {
            UIRoot = null;
            PlayerInput = null;
            GameCamera = null;
            ViewFactory = null;
            Configuration = null;
            Gameplay = null;
            Player = null;
            Level = null;
            GameHUD = null;
            PauseScreen = null;
            GameOverScreen = null;

            var resourceManager = GetResourceManager();
            resourceManager.ResetPools();
        }

        public static IResourceManager GetResourceManager()
        {
            if (ResourceManager == null)
            {
                ResourceManager = new ResourceManager();
            }

            return ResourceManager;
        }

        public static IGameCamera GetGameCamera()
        {
            if (GameCamera == null)
            {
                var resourceManager = GetResourceManager();
                GameCamera = resourceManager.CreatePrefabInstance<IGameCamera, EComponents>(EComponents.GameCamera);
            }

            return GameCamera;
        }

        public static ISceneLoader GetSceneLoader()
        {
            if (SceneLoader == null)
            {
                var gameObject = new GameObject("SceneLoader");
                SceneLoader = gameObject.AddComponent<SceneLoader>();
            }

            return SceneLoader;
        }

        public static IConfiguration GetConfiguration()
        {
            if (Configuration == null)
            {
                Configuration = new Configuration();
            }

            return Configuration;
        }

        public static IAudioManager GetAudioManager()
        {
            if (AudioManager == null)
            {
                var gameObject = new GameObject("AudioManager");
                AudioManager = gameObject.AddComponent<AudioManager>();
            }

            return AudioManager;
        }

        public static IUIRoot GetUIRoot()
        {
            if (UIRoot == null)
            {
                var resourceManager = GetResourceManager();
                UIRoot = resourceManager.CreatePrefabInstance<IUIRoot, EComponents>(EComponents.UIRoot);
            }

            return UIRoot;
        }

        public static IViewFactory GetViewFactory()
        {
            if (ViewFactory == null)
            {
                var uiRoot = GetUIRoot();
                var resourceManager = GetResourceManager();

                ViewFactory = new ViewFactory(uiRoot, resourceManager);
            }

            return ViewFactory;
        }

        public static IGameHUD GetGameHUD()
        {
            if (GameHUD == null)
            {
                var gameObject = new GameObject("GameHUD");
                GameHUD = gameObject.AddComponent<GameHUD>();
            }

            return GameHUD;
        }

        public static IPauseScreen GetPauseScreen()
        {
            if (PauseScreen == null)
            {
                var gameObject = new GameObject("PauseScreen");
                PauseScreen = gameObject.AddComponent<PauseScreen>();
            }

            return PauseScreen;
        }

        public static IGameOverScreen GetGameOverScreen()
        {
            if (GameOverScreen == null)
            {
                var gameObject = new GameObject("GameOverScreen");
                GameOverScreen = gameObject.AddComponent<GameOverScreen>();
            }

            return GameOverScreen;
        }

        public static IPlayerInput GetPlayerInput()
        {
            if (PlayerInput == null)
            {
                var gameObject = new GameObject("PlayerInput");
                PlayerInput = gameObject.AddComponent<PlayerInput>();
            }

            return PlayerInput;
        }

        public static IGameplay GetGameplay()
        {
            if (Gameplay == null)
            {
                var gameObject = new GameObject("Gameplay");
                Gameplay = gameObject.AddComponent<Gameplay>();
            }

            return Gameplay;
        }

        public static IPlayer GetPlayer()
        {
            if (Player == null)
            {
                var resourceManager = GetResourceManager();
                Player = resourceManager.CreatePrefabInstance<IPlayer, EUnits>(EUnits.SpaceShip);
            }

            return Player;
        }

        public static ILevel GetLevel()
        {
            if (Level == null)
            {
                var resourceManager = GetResourceManager();
                //TODO: Create level manager and pick level from configuration
                Level = resourceManager.CreatePrefabInstance<ILevel, ELevels>(ELevels.Level1);
            }

            return Level;
        }
    }
}
