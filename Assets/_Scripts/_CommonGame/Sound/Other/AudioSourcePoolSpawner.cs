using System.Collections;
using UnityEngine;
namespace CommonGame.Sound
{
    public class AudioSourcePoolSpawner : MonoPoolSpawner<AudioSource>
    {
        public int ExtendAmount = 20;
        public override IObjectPool<AudioSource> CreateFromPrefab(GameObject prefab)
        {
            ClearPool();
            IObjectPool<AudioSource> pool = new IObjectPool<AudioSource>(MaxPoolSize, null, null, OnReturned);

            for (int i = 0; i < MaxPoolSize; i++)
            {
                AudioSource target = Parent.gameObject.AddComponent<AudioSource>();
                pool.AddToPool(target);
            }
            CurrentPool = pool;
            return CurrentPool;
        }

        public override void OnTaken(AudioSource target)
        {

        }

        public override void OnReturned(AudioSource target)
        {
            if(this)
                target.clip = null;
        }

        public override void OnOut()
        {
            ExtendCurrentPool();
        }

        public void ExtendCurrentPool()
        {
            CurrentPool.MaxSize += ExtendAmount;
            for (int i = 0; i < ExtendAmount; i++)
            {
                AudioSource target = Parent.gameObject.AddComponent<AudioSource>();
                CurrentPool.AddToPool(target);
            }
        }

        public override void ClearPool()
        {
            if (CurrentPool != null)
            {
                foreach (AudioSource target in CurrentPool.Pool.Keys)
                {
                    if (target)
                    {
                        Destroy(target);
                    }
                }
            }
        }
    }
}