using UnityEngine;

namespace SpaceShootuh.Battle.Environment.Obstacle
{
    public interface IObstacle : IDamageable
    {
        void Go(Vector2 direction);
    }
}