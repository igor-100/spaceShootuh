using System.Collections.Generic;

namespace SpaceShootuh.Configurations
{
    public class EnemiesProperties
    {
        public List<EnemyProperties> Enemies;

        public class EnemyProperties
        {
            public EUnits Type;

            public float Health;
            public float Speed;
            public float Damage;
        }
    }
}