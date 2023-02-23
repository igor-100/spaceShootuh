using SpaceShootuh.Battle.Units;
using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using UnityEngine;

namespace SpaceShootuh.Battle.PowerUp
{
    public class PowerUp : MonoBehaviour, IPowerUp
    {
        private Rigidbody2D rigidBody;
        private PowerUpsProperties.PowerUpProperties properties;

        private CharacterStat speedStat;
        private float healthMod;

        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();

            properties = CompositionRoot.GetConfiguration().GetPowerUps().HealthPowerUp;

            if (properties == null)
            {
                Debug.LogError("ShortLazer props have not been set");
            }

            speedStat = new CharacterStat(properties.Speed);
            healthMod = properties.HealthModifier;
        }

        public void Go(Vector2 direction)
        {
            rigidBody.velocity = direction.normalized * speedStat.Value;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Border"))
            {
                gameObject.SetActive(false);
            }

            if (collision.gameObject.TryGetComponent<IPlayer>(out var player))
            {
                Collect(player);

                gameObject.SetActive(false);
            }
        }

        public void Collect(IPlayer player)
        {
            player.HealthStat.AddModifier(new CharacterStatModifier(healthMod, StatModType.PercentAdd));
            player.Heal(player.HealthStat.Value);
            Debug.Log($"Player has his maximum health increased (+{(int) (healthMod * 100)}%) and healed (Health: {player.Health}");
        }
    }
}