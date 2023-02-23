﻿using System.Collections.Generic;

namespace SpaceShootuh.Configurations
{
    public class Configuration : IConfiguration
    {
        private readonly PlayerProperties player;
        private readonly LevelProperties level;
        private readonly EnemiesProperties enemies;
        private readonly ProjectilesProperties projectiles;

        public Configuration()
        {
            player = new PlayerProperties()
            {
                Health = 100f,
                Speed = 7f
            };
            level = new LevelProperties()
            {
                MovementBorders = new LevelProperties.MovementBordersProperties()
                {
                    MinXOffset = -1.6f,
                    MaxXOffset = 1.6f,
                    MinYOffset = -3.6f,
                    MaxYOffset = 0f
                }
            };
            enemies = new EnemiesProperties()
            {
                Enemies = new List<EnemiesProperties.EnemyProperties>()
                {
                    new EnemiesProperties.EnemyProperties()
                    {
                        Type = EUnits.EnemyOne,
                        Health = 50f,
                        Speed = 2f,
                        Damage = 35f
                    }
                }
            };
            projectiles = new ProjectilesProperties()
            {
                ShortLazer = new ProjectilesProperties.ProjectileProperties()
                {
                    Damage = 10,
                    Speed = 7f
                },
                Ball_02 = new ProjectilesProperties.ProjectileProperties()
                {
                    Damage = 35,
                    Speed = 1f
                }
            };
        }

        public PlayerProperties GetPlayer() => player;
        public LevelProperties GetLevel() => level;
        public EnemiesProperties GetEnemies() => enemies;
        public ProjectilesProperties GetProjectiles() => projectiles;
    }
}