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
        [SerializeField] private string _widthPropertyName;
        [SerializeField] private float _widthChangeTime = 0.25f;


        public async void PlayLineEffect(Vector3 center, Vector3 dir, float distance, float time, CancellationToken token)
        {
            LineRenderer lineRend = Instantiate(_lineEffect);
            lineRend.gameObject.SetActive(true);
            lineRend.useWorldSpace = true;
            lineRend.SetPosition(0,center);
            lineRend.SetPosition(1, center + dir.normalized * distance);
            MaterialPropertyBlock block = new MaterialPropertyBlock();
            lineRend.GetPropertyBlock(block);
            #region WidthChanging
            float elapsed = 0f;
            while(elapsed <= _widthChangeTime)
            {
                if (token.IsCancellationRequested)
                {
                    DestroyLine(lineRend.gameObject);
                    return;
                }
                float width = Mathf.Lerp(0, 1, elapsed / _widthChangeTime) ;
                block.SetFloat(_widthPropertyName, width);
                lineRend.SetPropertyBlock(block);
                elapsed += Time.deltaTime;
                await Task.Yield();
            }
            block.SetFloat(_widthPropertyName, 1);
            lineRend.SetPropertyBlock(block);
            elapsed = 0;
            float minVal = -0.1f;
            while (elapsed <= _widthChangeTime)
            {
                if (token.IsCancellationRequested)
                {
                    DestroyLine(lineRend.gameObject);
                    return;
                }
                float width = Mathf.Lerp(1, -minVal, elapsed / _widthChangeTime);
                block.SetFloat(_widthPropertyName, width);
                lineRend.SetPropertyBlock(block);
                elapsed += Time.deltaTime;
                await Task.Yield();
            }
            #endregion
            if (token.IsCancellationRequested)
            {
                DestroyLine(lineRend.gameObject);
                return;
            }
            block.SetFloat(_widthPropertyName, minVal);
            lineRend.SetPropertyBlock(block);
            int lifeTime = (int)(1000 * (time - _widthChangeTime*2));
            if(lifeTime > 0)
                await Task.Delay(lifeTime);
            if(lineRend != null)
                Destroy(lineRend.gameObject);
        }

        private void DestroyLine(GameObject target)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                DestroyImmediate(target);
            }
            else
            {
                Destroy(target);
            }
#else
                Destroy(target);
#endif

        }
    }
}