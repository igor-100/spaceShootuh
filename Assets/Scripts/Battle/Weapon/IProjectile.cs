using UnityEngine;

namespace SpaceShootuh.Battle.Weapon
{
    public interface IProjectile : IDamageable
    {
        Vector3 Velocity { set; }
    }
}