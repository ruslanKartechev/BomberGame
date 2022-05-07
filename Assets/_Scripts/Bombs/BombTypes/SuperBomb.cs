using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonGame;
using System;
namespace BomberGame
{
    public class SuperBomb : BombBase
    {
        [SerializeField] private BombSettingsSO _settings;
        [Space(5)]
        [SerializeField] private BombLineEffectSO _effect;
        [SerializeField] private BombCountEffectBase _countEffect;
        private float _piercing;
        public override void InitCoundown()
        {
            _piercing = _settings.PiercingDepth;
            _countdown = StartCoroutine(Countdown(_settings.CountdownTime, Explode));
            _countEffect.StartCountdown(_settings.CountdownTime);
        }

        protected IEnumerator Countdown(float time, Action onFinish)
        {
            yield return new WaitForSeconds(time);
            onFinish.Invoke();
        }

        protected void Explode()
        {
            for (int i = 0; i < _settings.CastDirections.Count; i++)
            {
                float length = CastSide(_settings.CastDirections[i]);
                if (i == 0)
                    StartCoroutine(_effect.GetLine(transform.position, _settings.CastDirections[i], length, HideBomb));
                else
                    StartCoroutine(_effect.GetLine(transform.position, _settings.CastDirections[i], length, null));
            }
            OnExplode?.Invoke();
        }
        private void HideBomb()
        {
            Destroy(gameObject);
        }


        #region Raycasting
        protected float CastSide(Vector3 dir)
        {
            float distance = _settings.GridSize * _settings.StartExplosionLength;
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
            if (_settings.UsePiercingDepth)
            {
                return SwitchResultPiercing(result, distance);
            }
            else
                SwitchResultNoPiercing(result, distance);
            return distance;
        }

        protected virtual float SwitchResultPiercing(List<RaycastResult> result, float distance)
        {
            int pierced = 0;
            float lastDistance = distance;
            foreach (RaycastResult hit in result)
            {
                if (pierced < _piercing)
                {
                    IDamagable d = hit.GO.GetComponent<IDamagable>();
                    if (d != null)
                        d.TakeDamage(_settings.Damage, CharachterID);

                    IWall wall = hit.GO.GetComponent<IWall>();
                    if (wall == null)
                        continue;
                    switch (wall.GetType())
                    {
                        case WallType.Soft:
                            lastDistance = hit.Distance;
                            pierced += 1;
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
        protected virtual void SwitchResultNoPiercing(List<RaycastResult> result, float distance)
        {
            foreach (RaycastResult hit in result)
            {
                IDamagable d = hit.GO.GetComponent<IDamagable>();
                if (d != null)
                    d.TakeDamage(_settings.Damage,CharachterID);

            }
        }
        #endregion

    }
}