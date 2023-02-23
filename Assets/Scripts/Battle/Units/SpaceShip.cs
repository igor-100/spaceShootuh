using SpaceShootuh.Battle.Weapon;
using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using SpaceShootuh.Core.Controls;
using System;
using UnityEngine;

namespace SpaceShootuh.Battle.Units
{
    public class SpaceShip : MonoBehaviour, IPlayer
    {
        private const float FIRE_DELTA_TIME = 0.2f;

        private PlayerProperties playerConfig;
        private IResourceManager resourceManager;
        private IPlayerInput playerInput;
        private bool isFireAllowed;
        private float currentFireCooldownTime;

        private Borders borders;

        public event Action<IAlive> Died = (hero) => { };
        public event Action<float> HealthPercentChanged = (value) => { };

        public float Health { get; private set; }
        public CharacterStat HealthStat { get; private set; }
        public CharacterStat SpeedStat { get; private set; }

        private void Awake()
        {
            resourceManager = CompositionRoot.GetResourceManager();

            playerInput = CompositionRoot.GetPlayerInput();
            playerInput.MousePositionUpdated += OnMousePositionUpdated;
            playerInput.Fire += OnFire;

            playerConfig = CompositionRoot.GetConfiguration().GetPlayer();
            HealthStat = new CharacterStat(playerConfig.Health);
            SpeedStat = new CharacterStat(playerConfig.Speed);
            Health = HealthStat.Value;

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
                projectile.Shoot(transform.up);
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
            if (damage >= Health)
            {
                Health = 0;
                Debug.Log($"Player has been hit (damage: {damage}, health: {Health})");
                HealthPercentChanged(Health);
                Debug.Log("Player has been killed");
                Die();
            }
            else
            {
                Health -= damage;
                Debug.Log($"Player has been hit (damage: {damage}, health: {Health})");
                HealthPercentChanged(Health);
            }
        }

        private void Die()
        {
            Died(this);
            playerInput.MousePositionUpdated -= OnMousePositionUpdated;
            playerInput.Fire -= OnFire;
            Destroy(gameObject);
        }

        public void Heal(float value)
        {
            if (Health + value >= HealthStat.Value)
            {
                Health = HealthStat.Value;
            }
            else
            {
                Health += value;
            }
            HealthPercentChanged(Health);
        }
    }
}
