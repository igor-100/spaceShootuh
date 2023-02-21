using UnityEngine;

namespace SpaceShootuh.Core.Cameras
{
    public class GameCamera : MonoBehaviour, IGameCamera
    {
        [SerializeField]
        private Camera mainCamera;

        public Camera MainCamera => mainCamera;
    }   
}
