using System;
using UnityEngine;

namespace SpaceShootuh.Core.Controls
{
    public class PlayerInput : MonoBehaviour, IPlayerInput
    {
        private const string FireButton = "Fire1";

        public event Action Fire = () => { };
        public event Action Escape = () => { };
        public event Action<Vector3> MousePositionUpdated = mousePos => { };
        public event Action<Vector2> Move = moveVector => { };

        private void Update()
        {
            ListenToFire();
            ListenToEscape();
            ListenToMousePos();
            ListenToMove();
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
            MousePositionUpdated(Input.mousePosition);
        }

        private void ListenToMove()
        {
            var moveVector = Vector2.zero;
            moveVector.x = Input.GetAxisRaw("Horizontal");

            Move(moveVector.normalized);
        }
    }
}
