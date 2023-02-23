using Cysharp.Threading.Tasks;
using SpaceShootuh.Battle.Environment;
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
        private bool isTerminatingProcess;
        private CancellationTokenSource tokenSource;

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
        }

        public void SetLevelProperties(LevelProperties levelProperties)
        {
            this.levelProperties = levelProperties;
            var movementBorders = levelProperties.MovementBorders;

            player.SetMovementBorders(movementBorders.MinXOffset, movementBorders.MaxXOffset, movementBorders.MinYOffset, movementBorders.MaxYOffset);
        }

        private async UniTaskVoid SpawnEnemies()
        {
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

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
            TaskUtils.CancelToken(tokenSource);
        }
    }
}
