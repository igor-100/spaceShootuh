using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShootuh.Battle.Units
{
    public interface IEnemy : IAlive, IDamageable
    {
        void SetWaypoints(List<Vector2> waypoints);
        UniTask Go();
    }
}
