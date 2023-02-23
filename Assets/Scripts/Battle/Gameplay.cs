using Cysharp.Threading.Tasks;
using SpaceShootuh.Battle.Environment;
using SpaceShootuh.Battle.Environment.Obstacle;
using SpaceShootuh.Battle.PowerUp;
using SpaceShootuh.Battle.Units;
using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using SpaceShootuh.UI.GameHUD;
using SpaceShootuh.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceShootuh.Battle
{
    public class Gameplay : MonoBehaviour, IGameplay
    {
        private IPlayer player;
        private ILevel level;
        private IResourceManager resourceManager;
        private IGameHUD gameHud;
        private LevelProperties levelProperties;

        private int currentScore;
        private CancellationTokenSource enemiesTokenSource;
        private CancellationTokenSource obstaclesTokenSource;
        private CancellationTokenSource powerUpsTokenSource;

        public event Action<int> GameOver = (score) => { };

        private void Awake()
        {
            player = CompositionRoot.GetPlayer();
            level = CompositionRoot.GetLevel();
            resourceManager = CompositionRoot.GetResourceManager();
            gameHud = CompositionRoot.GetGameHUD();

            player.HealthPercentChanged += OnPlayerHealthChanged;
            player.Died += OnPlayerDied;
            SpawnEnemies().Forget();
            SpawnObstacles().Forget();
            SpawnPowerUps().Forget();
        }

        public void SetLevelProperties(LevelProperties levelProperties)
        {
            this.levelProperties = levelProperties;
            var movementBorders = levelProperties.MovementBorders;

            player.SetMovementBorders(movementBorders.MinXOffset, movementBorders.MaxXOffset, movementBorders.MinYOffset, movementBorders.MaxYOffset);
        }

        private async UniTaskVoid SpawnEnemies()
        {
            enemiesTokenSource = new CancellationTokenSource();
            var token = enemiesTokenSource.Token;

            // cycle waves due to having not so many of them manually configured
            int counter = 0;
            while (isActiveAndEnabled)
            {
                // increasing difficulty :)
                CharacterStatModifier healthModifier = null;
                if (counter > 0)
                {
                    healthModifier = new CharacterStatModifier(counter * 0.5f, StatModType.PercentAdd);
                }

                await SpawnWaves(token, healthModifier);
                counter++;
            }
        }

        private async UniTask SpawnWaves(CancellationToken token, CharacterStatModifier healthModifier)
        {
            foreach (var wave in level.WaveConfigs)
            {
                var tasks = new List<UniTask>();

                for (int i = 0; i < wave.NumberOfEnemies; i++)
                {
                    float spawnDelay = UnityEngine.Random.Range(wave.TimeBetweenSpawns - wave.SpawnRandomFactor, wave.TimeBetweenSpawns + wave.SpawnRandomFactor);
                    await UniTask.Delay((int)(spawnDelay * 1000), cancellationToken: token);
                    var enemy = resourceManager.GetPooledObject(wave.EnemyType).GetComponent<IEnemy>();
                    enemy.SetWaypoints(wave.Waypoints);

                    if (healthModifier != null)
                    {
                        enemy.HealthStat.AddModifier(healthModifier);
                        enemy.Heal(enemy.HealthStat.Value);
                    }

                    var task = enemy.Go();
                    tasks.Add(task);

                    enemy.Died += OnEnemyDied;
                }

                await UniTask.WhenAll(tasks);
            }
        }

        private async UniTaskVoid SpawnObstacles()
        {
            obstaclesTokenSource = new CancellationTokenSource();
            var token = obstaclesTokenSource.Token;
            while (isActiveAndEnabled)
            {
                float spawnDelay = UnityEngine.Random.Range(7f, 15f);
                await UniTask.Delay((int)(spawnDelay * 1000), cancellationToken: token);
                var obstacleGo = resourceManager.GetPooledObject(EObstacles.BluePlanet);
                obstacleGo.transform.position = new Vector2(UnityEngine.Random.Range(-2f, 2f), 8f);
                var obstacle = obstacleGo.GetComponent<IObstacle>();
                obstacle.Go(-transform.up);
            }
        }

        private async UniTaskVoid SpawnPowerUps()
        {
            powerUpsTokenSource = new CancellationTokenSource();
            var token = powerUpsTokenSource.Token;
            while (isActiveAndEnabled)
            {
                float spawnDelay = UnityEngine.Random.Range(10f, 20f);
                await UniTask.Delay((int)(spawnDelay * 1000), cancellationToken: token);
                var powerUpGo = resourceManager.GetPooledObject(EPowerUps.HealthPowerUp);
                powerUpGo.transform.position = new Vector2(UnityEngine.Random.Range(-2f, 2f), 8f);
                var powerUp = powerUpGo.GetComponent<IPowerUp>();
                powerUp.Go(-transform.up);
            }
        }

        private void OnEnemyDied(IAlive alive)
        {
            var enemy = (IEnemy)alive;
            currentScore += enemy.Score;
            gameHud.SetScore(currentScore);
            enemy.Died -= OnEnemyDied;
        }

        private void OnPlayerHealthChanged(float value)
        {
            gameHud.SetHealth(value / player.HealthStat.Value);
        }

        private void OnPlayerDied(IAlive alive)
        {
            player.Died -= OnPlayerDied;
            DelayedGameOver();
        }

        private async void DelayedGameOver()
        {
            await UniTask.Delay(2000);

            GameOver(currentScore);
        }

        private void OnDestroy()
        {
            TaskUtils.CancelToken(enemiesTokenSource);
            TaskUtils.CancelToken(obstaclesTokenSource);
            TaskUtils.CancelToken(powerUpsTokenSource);
        }
    }
}
