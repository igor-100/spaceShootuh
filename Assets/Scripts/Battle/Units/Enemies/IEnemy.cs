using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShootuh.Battle.Units
{
    public interface IEnemy : IAlive, IDamageable
    {
        int Score { get; }

        CharacterStat HealthStat { get; }

        void SetWaypoints(List<Vector2> waypoints);
        UniTask Go();
    }
}
