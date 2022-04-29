using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombLineEffectSO", menuName = "SO/BombLineEffectSO", order = 1)]
    public class BombLineEffectSO : ScriptableObject
    {
        [SerializeField] private float _time;
        [SerializeField] private LineRenderer _lineEffect;

        
        public IEnumerator GetLine(Vector3 center, Vector3 dir, float distance, Action onFinish)
        {
            return LineEffectExplosion(center,dir, distance, onFinish);
        }

        private IEnumerator LineEffectExplosion(Vector3 center, Vector3 dir, float distance, Action onFinish)
        {
            List<LineRenderer> lines = new List<LineRenderer>(4);
            LineRenderer effect = Instantiate(_lineEffect);
            effect.gameObject.SetActive(true);
            effect.useWorldSpace = true;
            effect.SetPosition(0,center);
            effect.SetPosition(1, center + dir.normalized * distance);
           
            yield return new WaitForSeconds(_time);
            onFinish?.Invoke();
            Destroy(effect.gameObject);
        }

    }
}