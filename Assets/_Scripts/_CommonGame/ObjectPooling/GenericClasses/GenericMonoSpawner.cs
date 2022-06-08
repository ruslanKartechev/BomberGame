using UnityEngine;
namespace CommonGame
{
    public class GenericMonoSpawner<T> : MonoPoolSpawner<IPoolSpawnable<T>> where T : class
    {
        public override IObjectPool<IPoolSpawnable<T>> CreateFromPrefab(GameObject prefab)
        {

            CurrentPool = new IObjectPool<IPoolSpawnable<T>>(MaxPoolSize, OnOut, OnTaken, OnReturned);
            for (int i = 0; i <= MaxPoolSize; i++)
            {
                GameObject go = Instantiate(prefab, Parent);

                go.SetActive(false);
                IPoolSpawnable<T> target = go.GetComponent<IPoolSpawnable<T>>();
                if(target == null)
                {
                    Debug.Log("IPoolSpawnable not found on the prefab");
                    return null;
                }
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
            foreach (IPoolSpawnable<T> go in CurrentPool.Pool.Keys)
            {
                if (go != null)
                    Destroy(go.GetGO());
                CurrentPool.ClearPool();
            }
        }

    }
}