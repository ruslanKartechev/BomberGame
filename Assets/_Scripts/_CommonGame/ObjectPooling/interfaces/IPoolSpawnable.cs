using System;
using UnityEngine;

namespace CommonGame
{
    public interface IPoolSpawnable<T> where T: class
    {
        void SetPool(IObjectPool<IPoolSpawnable<T>> pool);
        T GetObject();
        GameObject GetGO();
    }

}
