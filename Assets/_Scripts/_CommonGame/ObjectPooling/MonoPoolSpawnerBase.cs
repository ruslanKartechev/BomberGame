using UnityEngine;

namespace CommonGame
{
    public abstract class MonoPoolSpawner<T> : MonoBehaviour where T : class
    {
        public int MaxPoolSize = 30;
        public Transform Parent;

        public IObjectPool<T> CurrentPool;
        public abstract IObjectPool<T> CreateFromPrefab(GameObject prefab);
        public abstract void OnTaken(T target);
        public abstract void OnReturned(T target);
        public abstract void ClearPool();
        public abstract void OnOut();
    }
}