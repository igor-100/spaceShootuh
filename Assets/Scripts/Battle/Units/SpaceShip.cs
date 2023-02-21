using SpaceShootuh.Battle.Weapon;
using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using System;
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

        private Borders borders;

        public event Action<IAlive> Died;
        public event Action<float> HealthPercentChanged;

        public CharacterStat Health { get; private set; }
        public CharacterStat Speed { get; private set; }

        private void Awake()
        {
            resourceManager = CompositionRoot.GetResourceManager();

            var playerInput = CompositionRoot.GetPlayerInput();
            playerInput.MousePositionUpdated += OnMousePositionUpdated;
            playerInput.Fire += OnFire;

            playerConfig = CompositionRoot.GetConfiguration().GetPlayer();
            Health = new CharacterStat(playerConfig.Health);
            Speed = new CharacterStat(playerConfig.Speed);

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
            var targetMousePos = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

            var targetPos = Vector3.MoveTowards(transform.position, targetMousePos, playerConfig.Speed * Time.deltaTime);

            transform.position = new Vector3
                (
                Mathf.Clamp(targetPos.x, borders.MinX, borders.MaxX),
                Mathf.Clamp(targetPos.y, borders.MinY, borders.MaxY),
                0
                );
        }

        public void SetMovementBorders(float minXOffset, float maxXOffset, float minYOffset, float maxYOffset)
        {
            this.borders = new Borders(minXOffset, maxXOffset, minYOffset, maxYOffset);
        }

        public void Hit(float damage)
        {
            throw new NotImplementedException();
        }
    }
}
