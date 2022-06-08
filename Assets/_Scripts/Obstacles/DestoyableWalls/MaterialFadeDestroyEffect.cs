using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame
{
    public class MaterialFadeDestroyEffect : MonoBehaviour, IDestroyEffect, IHealthDisplay
    {
        [SerializeField] private string _fadeProperty;
        [SerializeField] private string _randomizedProperty;
        [SerializeField] private float _fadeTime;
        [SerializeField] private Renderer _rend;
        private Coroutine _fading;
        private bool _isDestoyed = false;
        private float _currentVal = 1;

        private void Start()
        {
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            _rend.GetPropertyBlock(properties);
            properties.SetFloat(_randomizedProperty, UnityEngine.Random.Range(-180f, 180f));
            _rend.SetPropertyBlock(properties);
        }

        public void DisplayHealth(float health)
        {
            if (_isDestoyed)
                return;
            _currentVal = health;
            Fade(_currentVal, health,null);
        }

        public void PlayDestroyEffect(Action onDone)
        {
            if (_isDestoyed)
                return;
            _isDestoyed = true;
            Fade(_currentVal, 0, onDone);
        }

        private void Fade(float start, float end, Action onDone)
        {
            if (_fading != null)
                StopCoroutine(_fading);
            end = Mathf.Clamp01(end);
            _fading = StartCoroutine(Fading(start, end, onDone));
        }

        private IEnumerator Fading(float startVal, float endVal, Action onDone)
        {
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            _rend.GetPropertyBlock(properties);
            float elapsed = 0f;
            float time = Mathf.Abs(startVal - endVal) * _fadeTime;
            while (elapsed <= time)
            {
                properties.SetFloat(_fadeProperty, Mathf.Lerp(startVal,endVal, elapsed / _fadeTime));
                _rend.SetPropertyBlock(properties);
                elapsed += Time.deltaTime;
                yield return null;
            }
            properties.SetFloat(_fadeProperty, endVal);
            _rend.SetPropertyBlock(properties);
            onDone?.Invoke();

        }
    }
}