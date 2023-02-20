using UnityEngine;
namespace SpaceShootuh.UI
{
    public interface IView
    {
        void Show();
        void Hide();
        void SetParent(Transform parentCanvas);
    }
}
