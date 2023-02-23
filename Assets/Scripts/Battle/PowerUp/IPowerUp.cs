using UnityEngine;

namespace SpaceShootuh.Battle.PowerUp
{
    public interface IPowerUp : ICollectable
    {
        void Go(Vector2 direction);
    }
}