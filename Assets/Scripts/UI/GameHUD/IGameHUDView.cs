namespace SpaceShootuh.UI.GameHUD
{
    public interface IGameHUDView : IView
    {
        void SetHP(float value);
        void SetScore(int value);
    }
}