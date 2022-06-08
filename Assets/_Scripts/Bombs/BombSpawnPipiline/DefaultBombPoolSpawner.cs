using CommonGame;
using UnityEngine;
using System;
using Zenject;
namespace BomberGame.Bombs
{

    public class DefaultBombPoolSpawner : GenericMonoSpawner<DefaultBombManager>, IBombPoolGetter
    {
        [Inject] private BombPoolChannelSO _bombChannel;
        [Inject] private DiContainer _container;

        public override IObjectPool<IPoolSpawnable<DefaultBombManager>> CreateFromPrefab(GameObject prefab)
        {

            CurrentPool = new IObjectPool<IPoolSpawnable<DefaultBombManager>>(MaxPoolSize, OnOut, OnTaken, OnReturned);
            for (int i = 0; i <= MaxPoolSize; i++)
            {
                GameObject go = _container.InstantiatePrefab(prefab);
                go.transform.parent = Parent;
                go.SetActive(false);
                IPoolSpawnable<DefaultBombManager> target = go.GetComponent<IPoolSpawnable<DefaultBombManager>>();
                if (target == null)
                {
                    Debug.Log("IPoolSpawnable not found on the prefab");
                    return null;
                }
                target.SetPool(CurrentPool);
                CurrentPool.AddToPool(target);
            }
            return CurrentPool;
        }

        public Bomb GetBombFromPool()
        {
            var spawnable = CurrentPool.TakeFromPool();
            if (spawnable != null)
                return spawnable.GetObject();
            else
            {
                throw new Exception("DefaultBombPoolSpawner: Out of bombs");
            }
        }

        private void Start()
        {
            _bombChannel._defPoolSpawner = this;
            _bombChannel.InitDefaultBombPool();
        }

    }
}