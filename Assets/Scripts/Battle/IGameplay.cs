using SpaceShootuh.Configurations;
using System;

namespace SpaceShootuh.Battle
{
    public interface IGameplay
    {
        event Action<int> GameOver;

        void SetLevelProperties(LevelProperties levelProperties);
    }
}
