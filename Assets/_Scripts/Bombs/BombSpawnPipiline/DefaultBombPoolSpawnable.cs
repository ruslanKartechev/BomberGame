using UnityEngine;
using CommonGame;
namespace BomberGame.Bombs
{
    public class DefaultBombPoolSpawnable : MonoBehaviour, IPoolSpawnable<DefaultBombManager>
    {
        public DefaultBombManager _bomb;

        private IObjectPool<IPoolSpawnable<DefaultBombManager>> _bombPool;

        private void OnEnable()
        {
            _bomb.OnBombDone += OnExploded;
        }

        private void OnDisable()
        {
            _bomb.OnBombDone -= OnExploded;
        }

        private void OnExploded()
        {
            _bombPool?.ReturnToPool(this);
        }

        GameObject IPoolSpawnable<DefaultBombManager>.GetGO()
        {
            return gameObject;
        }

        DefaultBombManager IPoolSpawnable<DefaultBombManager>.GetObject()
        {
            return _bomb;
        }

        void IPoolSpawnable<DefaultBombManager>.SetPool(IObjectPool<IPoolSpawnable<DefaultBombManager>> pool)
        {
            _bombPool = pool;
        }
    }
}