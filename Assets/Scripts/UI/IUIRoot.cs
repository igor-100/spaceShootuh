using UnityEngine;
namespace SpaceShootuh.UI
{
    public interface IUIRoot
    {
        Transform MainCanvas { get; }
        Transform OverlayCanvas { get; }
    }
}
