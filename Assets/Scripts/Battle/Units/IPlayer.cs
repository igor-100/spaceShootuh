namespace SpaceShootuh.Battle.Units
{
    public interface IPlayer : IAlive
    {
        float Health { get; }
        CharacterStat HealthStat { get; }
        CharacterStat SpeedStat { get; }

        void SetMovementBorders(float minXOffset, float maxXOffset, float minYOffset, float maxYOffset);
    }
    public class Borders
    {
        public float MinX { get; private set; }
        public float MaxX { get; private set; }
        public float MinY { get; private set; }
        public float MaxY { get; private set; }

        public Borders(float minX, float maxX, float minY, float maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }
    }
}
