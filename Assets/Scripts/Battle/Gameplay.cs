using Cysharp.Threading.Tasks;
using SpaceShootuh.Battle.Units;
using SpaceShootuh.Core;
using UnityEngine;

namespace SpaceShootuh.Battle
{
    public class Gameplay : MonoBehaviour, IGameplay
    {
        private IPlayer player;

        private void Awake()
        {
            player = CompositionRoot.GetPlayer();

            //SpawnEnemies();
        }
    }
}
