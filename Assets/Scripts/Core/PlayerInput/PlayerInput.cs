using System;
using UnityEngine;

namespace SpaceShootuh.Core.Controls
{
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        private const string FireButton = "Fire1";

        private Camera mainCamera;

        public event Action Fire = () => { };
        public event Action Escape = () => { };
        public event Action<Vector3> MousePositionUpdated = mousePos => { };
        public event Action<Vector3> MouseClicked = mousePos => { };

        private void Awake()
        {
            this.mainCamera = CompositionRoot.GetGameCamera().MainCamera;
        }

        private void Update()
        {
            ListenToFire();
            ListenToEscape();
            ListenToMousePos();
            ListenToMouseClick();
        }

        public void Enable()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }

        public void Disable()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }

        private void ListenToFire()
        {
            if (Input.GetButton(FireButton))
            {
                Fire();
            }
        }

        private void ListenToEscape()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Escape();
            }
        }

        private void ListenToMousePos()
        {
            MousePositionUpdated(mainCamera.ScreenToWorldPoint(Input.mousePosition));
        }

        private void ListenToMouseClick()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                MouseClicked(mousePosition);
            }
        }
    }
}
