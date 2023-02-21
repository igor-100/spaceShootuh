using System.Collections;
using UnityEngine;

namespace SpaceShootuh.Battle.Environment
{
    public class Borders
    {
        [Tooltip("offset from viewport borders for player's movement")]
        public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
        [HideInInspector] public float minX, maxX, minY, maxY;
    }
}