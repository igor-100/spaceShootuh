using SpaceShootuh.Battle.Units;
using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using UnityEngine;

namespace SpaceShootuh.Battle.Environment.Obstacle
{
    public class Obstacle : MonoBehaviour, IObstacle
    {
        private Rigidbody2D rigidBody;
        private ObstaclesProperties.ObstacleProperties properties;

        public float Damage => damageStat.Value;
        private CharacterStat damageStat;
        private CharacterStat speedStat;

        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();

            properties = CompositionRoot.GetConfiguration().GetObstacles().BluePlanet;

            if (properties == null)
            {
                Debug.LogError("Blueplanet props have not been set");
            }

            damageStat = new CharacterStat(properties.Damage);
            speedStat = new CharacterStat(properties.Speed);
        }

        public void Go(Vector2 direction)
        {
            rigidBody.velocity = direction.normalized * speedStat.Value;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IAlive>(out var aliveCol))
            {
                aliveCol.Hit(Damage);
            }

            if (collision.gameObject.CompareTag("Border"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}