using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using UnityEngine;

namespace SpaceShootuh.Battle.Weapon
{
    public class Ball : Projectile
    {
        private ProjectilesProperties.ProjectileProperties properties;

        protected override void Awake()
        {
            properties = CompositionRoot.GetConfiguration().GetProjectiles().Ball_02;

            if (properties == null)
            {
                Debug.LogError("Ball props have not been set");
            }

            base.Awake();
        }

        protected override CharacterStat SetDamage() => new CharacterStat(properties.Damage);
        protected override CharacterStat SetSpeed() => new CharacterStat(properties.Speed);
    }
}