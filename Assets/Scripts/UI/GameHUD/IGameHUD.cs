namespace SpaceShootuh.UI.GameHUD
{
    public interface IGameHUD : IScreen
    {
        void SetHealth(float value);
        void SetScore(int value);
    }
}