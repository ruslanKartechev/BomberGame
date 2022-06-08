using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonGame
{
    public class ParticleLauncherPoolManager : MonoBehaviour
    {
        public int InitialCount = 20; 
        public GameObject _particlePrefab;
        [SerializeField] private ParticleLauncherObjectPoolSpawner _poolSpawner;
        private IObjectPool<IPoolSpawnable<ParticlesLauncher>> _particlesPool;

        private void Start()
        {
            _poolSpawner.MaxPoolSize = InitialCount;
            _particlesPool = _poolSpawner.CreateFromPrefab(_particlePrefab);
        }

        public ParticlesLauncher GetParticleLauncher()
        {
            var r = _particlesPool.TakeFromPool();
            if (r != null)
                return r.GetObject();
            throw new System.Exception("Out of pool");
        }


    }
}