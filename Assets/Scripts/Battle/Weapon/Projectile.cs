using UnityEngine;

namespace SpaceShootuh.Battle.Weapon
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private float damage = 20f;
        private Rigidbody2D rigidBody;

        public float Damage { get => damage; }
        public Vector3 Velocity
        {
            set
            {
                rigidBody.velocity = value;
            }
        }

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            gameObject.SetActive(false);
        }
    }
}