namespace SpaceShootuh.Configurations
{
    public class Configuration : IConfiguration
    {
        private readonly PlayerProperties player;
        private readonly LevelProperties level;

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
        }

        public PlayerProperties GetPlayer() => player;
        public LevelProperties GetLevel() => level;
    }
}