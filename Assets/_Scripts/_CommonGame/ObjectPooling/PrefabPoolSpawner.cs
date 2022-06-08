using UnityEngine;
namespace CommonGame
{
    public abstract class PrefabPoolSpawner<T> : PoolSpawnerBase where T: class
    {

        public Transform Parent;
        public IGameobjectSpawner GOSpawner;
        public PrefabPoolSpawner(IGameobjectSpawner spawner)
        {
            GOSpawner = spawner;
        }
        public IObjectPool<T> CurrentPool;
        public abstract IObjectPool<T> CreateFromPrefab(GameObject prefab);
        public abstract void OnTaken(T target);
        public abstract void OnReturned(T target);
        public abstract void ClearPool();
        public abstract void OnOut();
    }
    

}
