using SpaceShootuh.Battle.Weapon;
using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using UnityEngine;

namespace SpaceShootuh.Battle.Units
{
    public class SpaceShip : MonoBehaviour, IPlayer
    {
        private const float FIRE_DELTA_TIME = 0.1f;
        private const float FIRE_SPEED = 2f;

        private PlayerProperties playerConfig;
        private IResourceManager resourceManager;

        private bool isFireAllowed;
        private float currentFireCooldownTime;

        private void Awake()
        {
            resourceManager = CompositionRoot.GetResourceManager();

            var playerInput = CompositionRoot.GetPlayerInput();
            playerInput.MousePositionUpdated += OnMousePositionUpdated;
            playerInput.Fire += OnFire;

            playerConfig = CompositionRoot.GetConfiguration().GetPlayer();

            isFireAllowed = true;
        }

        private void Update()
        {
            if (!isFireAllowed)
            {
                currentFireCooldownTime += Time.deltaTime;
                if (currentFireCooldownTime >= FIRE_DELTA_TIME)
                {
                    isFireAllowed = true;
                    currentFireCooldownTime = 0;
                }
            }
        }

        private void OnFire()
        {
            if (isFireAllowed)
            {
                var projectileObj = resourceManager.GetPooledObject(EProjectiles.ShortLazer);
                projectileObj.transform.position = transform.position + transform.up * 0.3f;

                var projectile = projectileObj.GetComponent<IProjectile>();
                projectile.Velocity = transform.up * FIRE_SPEED;
                isFireAllowed = false;
            }
        }

        private void OnMousePositionUpdated(Vector3 mousePosition)
        {
            var targetPos = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, playerConfig.Speed * Time.deltaTime);
        }
    }
}
