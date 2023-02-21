using System;
using UnityEngine;

namespace SpaceShootuh.Core.Controls
{
    public interface IPlayerInput
    {
        event Action Fire;
        event Action Escape;
        event Action<Vector3> MousePositionUpdated;
        event Action<Vector3> MouseClicked;

        void Disable();
        void Enable();
    }
}

