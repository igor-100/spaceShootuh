using Cysharp.Threading.Tasks;
using SpaceShootuh.Battle.Environment;
using SpaceShootuh.Battle.Units;
using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShootuh.Battle
{
    public class Gameplay : MonoBehaviour, IGameplay
    {
        private IPlayer player;
        private ILevel level;
        private IResourceManager resourceManager;
        private LevelProperties levelProperties;

        private void Awake()
        {
            player = CompositionRoot.GetPlayer();
            level = CompositionRoot.GetLevel();
            resourceManager = CompositionRoot.GetResourceManager();

            SpawnEnemies();
        }

        public void SetLevelProperties(LevelProperties levelProperties)
        {
            this.levelProperties = levelProperties;
            var movementBorders = levelProperties.MovementBorders;

            player.SetMovementBorders(movementBorders.MinXOffset, movementBorders.MaxXOffset, movementBorders.MinYOffset, movementBorders.MaxYOffset);
        }

        private async void SpawnEnemies()
        {
            foreach (var wave in level.WaveConfigs)
            {
                var tasks = new List<UniTask>();

                for (int i = 0; i < wave.NumberOfEnemies; i++)
                {
                    float spawnDelay = UnityEngine.Random.Range(wave.TimeBetweenSpawns - wave.SpawnRandomFactor, wave.TimeBetweenSpawns + wave.SpawnRandomFactor);
                    await UniTask.Delay((int)(spawnDelay * 1000));
                    var enemy = resourceManager.GetPooledObject(wave.EnemyType).GetComponent<IEnemy>();
                    enemy.SetWaypoints(wave.Waypoints);

                    var task = enemy.Go();
                    tasks.Add(task);
                }

                await UniTask.WhenAll(tasks);
            }
        }
    }
}
