using SpaceShootuh.Battle.Units;
using SpaceShootuh.Configurations;
using SpaceShootuh.Core;
using UnityEngine;

namespace SpaceShootuh.Battle
{
    public class Gameplay : MonoBehaviour, IGameplay
    {
        private IPlayer player;
        private LevelProperties levelProperties;

        private void Awake()
        {
            player = CompositionRoot.GetPlayer();
            _ = CompositionRoot.GetLevel();
        }

        public void SetLevelProperties(LevelProperties levelProperties)
        {
            this.levelProperties = levelProperties;
            var movementBorders = levelProperties.MovementBorders;

            player.SetMovementBorders(movementBorders.MinXOffset, movementBorders.MaxXOffset, movementBorders.MinYOffset, movementBorders.MaxYOffset);
        }
    }
}
