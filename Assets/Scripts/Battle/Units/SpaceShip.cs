using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using UnityEngine;

namespace SpaceShootuh.Battle.Units
{
    public class SpaceShip : MonoBehaviour, IPlayer
    {
        private PlayerProperties playerConfig;

        private void Awake()
        {
            var playerInput = CompositionRoot.GetPlayerInput();
            playerInput.MouseClicked += OnMouseClicked;

            playerConfig = CompositionRoot.GetConfiguration().GetPlayer();
        }

        private void OnMouseClicked(Vector3 mousePosition)
        {
            var targetPos = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, playerConfig.Speed * Time.deltaTime);
        }
    }
}
