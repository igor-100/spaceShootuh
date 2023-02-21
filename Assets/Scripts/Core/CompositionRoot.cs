using SpaceShootuh.Configurations;
using SpaceShootuh.Core.Audio;
using SpaceShootuh.Core.Cameras;
using SpaceShootuh.Core.Controls;
using SpaceShootuh.UI;
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

        private void OnDestroy()
        {
            UIRoot = null;
            PlayerInput = null;
            GameCamera = null;
            ViewFactory = null;
            Configuration = null;

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

        public static IPlayerInput GetPlayerInput()
        {
            if (PlayerInput == null)
            {
                var gameObject = new GameObject("PlayerInput");
                PlayerInput = gameObject.AddComponent<PlayerInput>();
            }

            return PlayerInput;
        }
    }
}
