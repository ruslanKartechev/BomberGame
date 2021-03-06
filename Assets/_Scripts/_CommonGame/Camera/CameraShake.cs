using System.Collections;
using UnityEngine;
namespace CommonGame
{

    public class CameraShake : MonoBehaviour
    {
        [Header("Event Channels")]
        public CameraShakeChannelSO _camShakeChannel;
        [Header("Settings")]
        [SerializeField] private CamShakeSettings _settings;
        private Coroutine _shaking;
        private Vector3 startLocalPosition;

        private void Start()
        {
            _camShakeChannel.ShakeCamera = Shake;
        }

        public void Shake()
        {
            if (_shaking != null) StopCoroutine(_shaking);
            _shaking = StartCoroutine(Shaking(_settings.Duration, _settings.Magnitude));
        }

        public void StopShaking()
        {
            transform.localPosition = startLocalPosition;
            if (_shaking != null) StopCoroutine(_shaking);
            _shaking = null;
        }

        private IEnumerator Shaking(float duration, float magnitude)
        {
            float elapsed = 0f;
            startLocalPosition = transform.localPosition;
            while(elapsed <= duration)
            {
                transform.localPosition = startLocalPosition + Random.onUnitSphere * magnitude;
                elapsed += Time.deltaTime;
                yield return null;
            }
            StopShaking();
        }


    }
}