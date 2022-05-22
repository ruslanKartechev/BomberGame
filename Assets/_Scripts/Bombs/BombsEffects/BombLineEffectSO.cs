using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombLineEffectSO", menuName = "SO/BombLineEffectSO", order = 1)]
    public class BombLineEffectSO : ScriptableObject
    {
        [SerializeField] private LineRenderer _lineEffect;


        public async Task PlayLineEffect(Vector3 center, Vector3 dir, float distance, float time)
        {
            List<LineRenderer> lines = new List<LineRenderer>(4);
            LineRenderer effect = Instantiate(_lineEffect);
            effect.gameObject.SetActive(true);
            effect.useWorldSpace = true;
            effect.SetPosition(0,center);
            effect.SetPosition(1, center + dir.normalized * distance);

            await Task.Delay((int)(1000*time));
            if(effect != null)
                Destroy(effect.gameObject);
        }

    }
}