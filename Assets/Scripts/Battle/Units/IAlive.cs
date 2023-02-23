using System;

namespace SpaceShootuh.Battle.Units
{
    public interface IAlive
    {
        event Action<IAlive> Died;
        event Action<float> HealthPercentChanged;
        void Hit(float damage);
        void Heal(float value);
    }
}
