using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class FlickerDamageEffect : DamageEffectBase
    {
        public Color FlickColor;
        public float Duration;
        public SpriteRenderer Rend;
        private Color normalColor;
        private Coroutine _flickering;
        private void Start()
        {
            normalColor = Rend.color;
        }
        public override void Execute()
        {
            if (_flickering != null) StopCoroutine(_flickering);
            _flickering = StartCoroutine(Flickering());
        }
        private IEnumerator Flickering()
        {
            float elapsed = 0f;
            int flickNum = 7;
            float flickTime = Duration / flickNum;
            int n = 0;
            while (n <= flickNum)
            {
                if (n % 2 == 0)
                    Rend.color = FlickColor;
                else
                    Rend.color = normalColor;
                while (elapsed <= flickTime)
                {
                    elapsed += Time.deltaTime;
                    yield return null;
                }
                elapsed = 0f;
                n++;
            }
            Rend.color = normalColor;

        }
    }
}
