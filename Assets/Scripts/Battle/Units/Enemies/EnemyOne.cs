using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using SpaceShootuh.Battle.Weapon;
using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using SpaceShootuh.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace SpaceShootuh.Battle.Units
{
    public class EnemyOne : MonoBehaviour, IEnemy
    {
        private const float FIRE_DELTA_TIME = 2f;
        private const float RANDOM_FIRE = 0.5f;

        private EnemiesProperties.EnemyProperties properties;
        private List<Vector2> waypoints;
        private IResourceManager resourceManager;

        private TweenerCore<Vector3, Vector3, VectorOptions> movingTween;
        private CancellationTokenSource cancellationTokenSource;
        private bool isDying;

        public event Action<IAlive> Died = (enemy) => { };
        public event Action<float> HealthPercentChanged = (value) => { };

        public float Health { get; private set; }
        public float Damage => DamageStat.Value;
        public CharacterStat HealthStat { get; private set; }
        public CharacterStat DamageStat { get; private set; }
        public CharacterStat SpeedStat { get; private set; }

        private void Awake()
        {
            resourceManager = CompositionRoot.GetResourceManager();
            var configuration = CompositionRoot.GetConfiguration();
            properties = configuration.GetEnemies().Enemies.Find(enemy => enemy.Type == EUnits.EnemyOne);
        }

        private void OnEnable()
        {
            if (properties != null)
            {
                HealthStat = new CharacterStat(properties.Health);
                DamageStat = new CharacterStat(properties.Damage);
                SpeedStat = new CharacterStat(properties.Speed);

                Health = HealthStat.Value;
            }
            else
            {
                Debug.LogError("EnemyOne props have not been set");
            }
        }

        public void Hit(float damage)
        {
            if (damage >= Health)
            {
                Die();
            }
            else
            {
                Health -= damage;
                HealthPercentChanged(Health);
            }
        }

        private void Die()
        {
            if (isDying)
                return;
            isDying = true;
            TaskUtils.CancelToken(cancellationTokenSource);
            isDying = false;

            Died(this);
            gameObject.SetActive(false);
        }

        public void SetWaypoints(List<Vector2> waypoints)
        {
            this.waypoints = waypoints;
        }

        public async UniTask Go()
        {
            transform.position = waypoints[0];
            Shoot();

            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            foreach (var waypoint in waypoints)
            {
                if (!token.IsCancellationRequested)
                {
                    movingTween = transform.DOMove(waypoint, SpeedStat.Value).SetEase(Ease.Linear).SetSpeedBased();
                    await movingTween.ToUniTaskWithImmediateCancel(token);
                }
            }

            if (!token.IsCancellationRequested)
            {
                DieNaturally();
            }
        }

        private async void Shoot()
        {
            while (isActiveAndEnabled)
            {
                var projectileObj = resourceManager.GetPooledObject(EProjectiles.Ball);
                projectileObj.transform.position = transform.position - transform.up * 0.3f;

                var projectile = projectileObj.GetComponent<IProjectile>();
                projectile.Shoot(-transform.up);

                float fireDelay = UnityEngine.Random.Range(FIRE_DELTA_TIME - RANDOM_FIRE, FIRE_DELTA_TIME + RANDOM_FIRE);
                await UniTask.Delay((int)(fireDelay * 1000));
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IAlive>(out var aliveCol))
            {
                aliveCol.Hit(Damage);

                Die();
            }
        }

        private void DieNaturally()
        {
            if (isDying)
                return;
            isDying = true;
            TaskUtils.CancelToken(cancellationTokenSource);

            gameObject.SetActive(false);
        }
    }
}
