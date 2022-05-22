using System.Collections.Generic;
using UnityEngine;
namespace CommonGame.Sound
{

    public class AudioSourceManager : MonoBehaviour, IAudioSourceManager
    {
        private IObjectPool<AudioSource> _sourcesPool;
        [SerializeField] private AudioSourcePoolSpawner _poolSpawner;
        [SerializeField] private int _effectsCount = 30;

        public void Init()
        {
            _sourcesPool = _poolSpawner.CreateFromPrefab(null);
        }

        public AudioSource GetSource()
        {
            _poolSpawner.MaxPoolSize = _effectsCount;
            AudioSource source = _sourcesPool.TakeFromPool();
            if(source == null)
            {
                _poolSpawner.ExtendCurrentPool();
            }
            source = _sourcesPool.TakeFromPool();
            return source;
        }

        public void ReleaseSource(AudioSource source)
        {
            _sourcesPool.ReturnToPool(source);
        }



    }
}