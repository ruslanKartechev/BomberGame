using System;
using UnityEngine;
namespace CommonGame
{
    public class GenericPoolSpawner<T> : PrefabPoolSpawner<IPoolSpawnable<T>> where T : class
    {
        public GenericPoolSpawner(IGameobjectSpawner spawner) : base(spawner)
        {
        }
        public override IObjectPool<IPoolSpawnable<T>> CreateFromPrefab(GameObject prefab)
        {
            if (GOSpawner == null)
                return null;
            CurrentPool = new IObjectPool<IPoolSpawnable<T>>(MaxPoolSize, OnOut, OnTaken, OnReturned);
            for (int i = 0; i <= MaxPoolSize; i++)
            {
                GameObject go = GOSpawner.InstantiatePF(prefab,Parent);

                go.SetActive(false);
                IPoolSpawnable<T> target = go.GetComponent<IPoolSpawnable<T>>();
                target.SetPool(CurrentPool);
                CurrentPool.AddToPool(target);
            }
            return CurrentPool;
        }


        public override void OnOut()
        {
            Debug.Log("Out out pool");
        }

        public override void OnReturned(IPoolSpawnable<T> target)
        {
            target?.GetGO().SetActive(false);
        }

        public override void OnTaken(IPoolSpawnable<T> target)
        {
            target?.GetGO().SetActive(true);
        }

        public override void ClearPool()
        {
            if (GOSpawner == null)
                return;
            foreach (IPoolSpawnable<T> go in CurrentPool.Pool.Keys)
            {
                if(go!=null)
                    GOSpawner.DestroyGO(go.GetGO());
                CurrentPool.ClearPool();
            }
        }

    }
}