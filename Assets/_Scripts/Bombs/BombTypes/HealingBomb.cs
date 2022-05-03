using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame
{
    public class HealingBomb : BombBase
    {
        [SerializeField] private BombSettingsSO _settings;
        [Space(5)]
        [SerializeField] private BombLineEffectSO _effect;
        [SerializeField] private BombCountEffectBase _countEffect;

        private float _explosionLength;
        private int _piercing = 1;
        public override void InitCoundown()
        {
            _piercing = _settings.PiercingDepth;
            _explosionLength = _settings.StartExplosionLength;
            _countdown = StartCoroutine(Countdown(_settings.CountdownTime, Explode));
            _countEffect.StartCountdown(_settings.CountdownTime);
        }

        protected  IEnumerator Countdown(float time, Action onFinish)
        {
            yield return new WaitForSeconds(time);
            onFinish.Invoke();
        }
        protected void Explode()
        {
            float length = CastSide(_settings.CastDirections[0]);
            StartCoroutine(_effect.GetLine(transform.position, _settings.CastDirections[0], length, HideBomb));
            for (int i = 1; i < _settings.CastDirections.Count; i++)
            {
                length = CastSide(_settings.CastDirections[i]);
                StartCoroutine(_effect.GetLine(transform.position, _settings.CastDirections[i], length, null));
            }
        }
        private void HideBomb()
        {
            Destroy(gameObject);
        }
        #region Raycasting
        protected float CastSide(Vector3 dir)
        {
            float distance = _settings.GridSize * _explosionLength;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _settings.CircleCastRad, dir, distance, _settings.CastMask);
            if (hits.Length == 0)
                return distance;
            List<RaycastResult> result = new List<RaycastResult>(hits.Length);
            foreach (RaycastHit2D h in hits)
            {
                if (h == true)
                    result.Add(new RaycastResult(h.collider.gameObject, h.distance));
            }
            result.Sort();
            RaycastResult fixedWall = result.Find(t => t.GO.tag == Tags.StaticWall);
            if (fixedWall != null)
            {
                distance = fixedWall.Distance;
            }
            SwitchResultPiercing(result, distance);

            return distance;
        }
        protected float SwitchResultPiercing(List<RaycastResult> result, float distance)
        {
            int pierced = 0;
            float lastDistance = distance;
            foreach (RaycastResult hit in result)
            {
                if (pierced < _piercing)
                {
                    switch (hit.GO.tag)
                    {
                        case Tags.Charachter:
                            CharachterEffect(hit.GO);
                            break;
                        default:
                            if (hit.Distance <= distance)
                            {
                                lastDistance = hit.Distance;
                                pierced += 1;
                            }
                            break;
             
                    }
                }
                else
                {
                    return lastDistance;
                }
            }
            return lastDistance;
        }
        #endregion

        protected void CharachterEffect(GameObject go)
        {
            Debug.Log("Healing charachter");
            IHealable h = go.GetComponent<IHealable>();
            if(h != null)
                h.Heal(1);
        }

    }
}