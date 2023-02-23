using System.Collections.Generic;
using UnityEngine;

namespace SpaceShootuh.Configurations
{
    [CreateAssetMenu(menuName = "Enemy Wave Config")]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private EUnits enemyType;
        [SerializeField] private GameObject pathPrefab;
        [SerializeField] private float timeBetweenSpawns = 0.5f;
        [SerializeField] private float spawnRandomFactor = 0.3f;
        [SerializeField] private int numberOfEnemies = 10;

        public EUnits EnemyType => enemyType;
        public List<Vector2> Waypoints
        {
            get
            {
                var waveWaypoints = new List<Vector2>();
                foreach (Transform item in pathPrefab.transform)
                {
                    waveWaypoints.Add(item.position);
                }

                return waveWaypoints;
            }
        }

        public float TimeBetweenSpawns => timeBetweenSpawns;
        public float SpawnRandomFactor => spawnRandomFactor;
        public float NumberOfEnemies => numberOfEnemies;
    }
}