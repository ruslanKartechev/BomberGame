using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
namespace CommonGame
{
    public class ParticleLauncherPoolVersion : ParticlesLauncher, IPoolSpawnable<ParticlesLauncher>
    {
        private IObjectPool<IPoolSpawnable<ParticlesLauncher>> _pool;
        [SerializeField] private float _poolReturnTime = 2f;
        public GameObject GetGO()
        {
            return gameObject;
        }

        public ParticlesLauncher GetObject()
        {
            return this;
        }

        public void SetPool(IObjectPool<IPoolSpawnable<ParticlesLauncher>> pool)
        {
            _pool = pool;
        }

        public override async void Play()
        {
            base.Play();
            await Task.Delay((int)(1000 * _poolReturnTime));
            if (this != null)
                _pool?.ReturnToPool(this);
        }
    }
}