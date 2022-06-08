using UnityEngine;
using System.Collections.Generic;
using System.Threading;
namespace BomberGame.Bombs
{
    public class LineExplosionEffect : MonoBehaviour
    {
        [SerializeField] private BombLineEffectSO _lineEffect;
        private CancellationTokenSource _tokenSource;

        private void OnEnable()
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
        }
        private void OnDisable()
        {
            _tokenSource.Cancel();
        }

        public void Play(Vector2 center, Vector2 dir, float length, float duration)
        {
            try
            {
                _lineEffect?.PlayLineEffect(center, dir, length, duration, _tokenSource.Token);
            }
            catch
            {
                Debug.Log("cancelled effect");
            }
        }

    }
}