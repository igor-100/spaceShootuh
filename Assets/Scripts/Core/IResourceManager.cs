using System;
using UnityEngine;

namespace SpaceShootuh.Core
{
    public interface IResourceManager
    {
        T CreatePrefabInstance<T, E>(E item) where E : Enum;
        GameObject CreatePrefabInstance<E>(E item) where E : Enum;
        T GetAsset<T, E>(E item) where T : UnityEngine.Object where E : Enum;
        GameObject GetPooledObject<T, E>(E item) where E : Enum;
        GameObject GetPooledObject<T, E>(E item, int maximumSize) where E : Enum;
        void ResetPools();
    }
}
