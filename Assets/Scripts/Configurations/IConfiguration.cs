namespace SpaceShootuh.Configurations
{
    public interface IConfiguration
    {
        PlayerProperties GetPlayer();
        LevelProperties GetLevel();
        EnemiesProperties GetEnemies();
        ProjectilesProperties GetProjectiles();
        ObstaclesProperties GetObstacles();
        PowerUpsProperties GetPowerUps();
    }
}
