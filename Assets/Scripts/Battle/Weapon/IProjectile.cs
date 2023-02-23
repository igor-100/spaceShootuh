using UnityEngine;

namespace SpaceShootuh.Battle.Weapon
{
    public interface IProjectile : IDamageable
    {
        void Shoot(Vector2 direction);
    }
}