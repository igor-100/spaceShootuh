namespace SpaceShootuh.Configurations
{
    public class Configuration : IConfiguration
    {
        private readonly PlayerProperties player;

        public Configuration()
        {
            player = new PlayerProperties()
            {
                Health = 100f,
                Speed = 7f
            };
        }

        public PlayerProperties GetPlayer() => player;
    }
}