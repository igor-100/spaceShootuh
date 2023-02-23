using Cysharp.Threading.Tasks;
using SpaceShootuh.Battle.Environment;
using SpaceShootuh.Battle.Environment.Obstacle;
using SpaceShootuh.Battle.Units;
using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using SpaceShootuh.UI.GameHUD;
using SpaceShootuh.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
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

            foreach (var wave in level.WaveConfigs)
            {
                var tasks = new List<UniTask>();

                for (int i = 0; i < wave.NumberOfEnemies; i++)
                {
                    float spawnDelay = UnityEngine.Random.Range(wave.TimeBetweenSpawns - wave.SpawnRandomFactor, wave.TimeBetweenSpawns + wave.SpawnRandomFactor);
                    await UniTask.Delay((int)(spawnDelay * 1000), cancellationToken: token);
                    var enemy = resourceManager.GetPooledObject(wave.EnemyType).GetComponent<IEnemy>();
                    enemy.SetWaypoints(wave.Waypoints);

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
            var token = enemiesTokenSource.Token;
            while (isActiveAndEnabled)
            {
                float spawnDelay = UnityEngine.Random.Range(5f, 10f);
                await UniTask.Delay((int)(spawnDelay * 1000), cancellationToken: token);
                var obstacleGo = resourceManager.GetPooledObject(EObstacles.BluePlanet);
                obstacleGo.transform.position = new Vector2(UnityEngine.Random.Range(-2f, 2f), 8f);
                var obstacle = obstacleGo.GetComponent<IObstacle>();
                obstacle.Go(-transform.up);
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
        }
    }
}
