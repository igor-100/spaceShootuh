using SpaceShootuh.Battle.Units;
using UnityEngine;

namespace SpaceShootuh.Battle.Weapon
{
    public abstract class Projectile : MonoBehaviour, IProjectile
    {
        private Rigidbody2D rigidBody;

        public float Damage => damageStat.Value;
        private CharacterStat damageStat;
        private CharacterStat speedStat;

        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();

            damageStat = SetDamage();
            speedStat = SetSpeed();
        }

        public void Shoot(Vector2 direction)
        {
            rigidBody.velocity = direction.normalized * speedStat.Value;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IAlive>(out var aliveCol))
            {
                aliveCol.Hit(Damage);
            }

            gameObject.SetActive(false);
        }

        protected abstract CharacterStat SetSpeed();
        protected abstract CharacterStat SetDamage();
    }
}