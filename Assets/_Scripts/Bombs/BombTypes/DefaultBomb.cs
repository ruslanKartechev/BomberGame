using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CommonGame;
namespace BomberGame
{
    public class DefaultBomb : BombBase, ILengthBuffable, IPierceBuffable
    {
        [SerializeField] private BombSettingsSO _settings;
        [Space(5)]
        [SerializeField] private BombLineEffectSO _effect;
        [SerializeField] private BombCountEffectBase _countEffect;
        [Space(5)]
        [SerializeField] private CameraShakeChannelSO _camShakeChannel;

        
        private float _explosionLength;
        private int _piercing;
        private bool _limitPiercing;
        
        public override void InitCoundown()
        {
            _explosionLength = _settings.StartExplosionLength;
            _piercing = _settings.PiercingDepth;
            _limitPiercing = _settings.UsePiercingDepth;
            _countdown = StartCoroutine(Countdown(_settings.CountdownTime, Explode));
            _countEffect.StartCountdown(_settings.CountdownTime);
        }

        protected virtual IEnumerator Countdown(float time, Action onFinish)
        {
            yield return new WaitForSeconds(time);
            onFinish.Invoke();
        }

        protected virtual void Explode()
        {
            _camShakeChannel?.RaiseEventCameraShake();
            for(int i = 0; i < _settings.CastDirections.Count; i++)
            {
                float length = CastSide(_settings.CastDirections[i]);
                if(i ==0)
                    StartCoroutine(_effect.GetLine(transform.position, _settings.CastDirections[i], length, HideBomb));
                else
                    StartCoroutine(_effect.GetLine(transform.position, _settings.CastDirections[i], length, null));
            }
        }
        private void HideBomb()
        {
            Destroy(gameObject);
        }


        #region Raycasting
        protected virtual float CastSide(Vector3 dir)
        {
            float distance = _settings.GridSize * _explosionLength;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position,_settings.CircleCastRad, dir, distance, _settings.CastMask);
            if (hits.Length == 0)
                return distance;
            List<RaycastResult> result = new List<RaycastResult>(hits.Length);
            foreach (RaycastHit2D h in hits)
            {
                if(h == true)
                    result.Add(new RaycastResult(h.collider.gameObject, h.distance));
            }
            result.Sort();
            RaycastResult fixedWall = result.Find(t => t.GO.tag == Tags.StaticWall);
            if (fixedWall != null)
            {
                distance = fixedWall.Distance;
            }
            if (_limitPiercing)
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
                        d.TakeDamage(1);

                    IWall wall = hit.GO.GetComponent<IWall>();
                    if (wall == null)
                        continue;
                    switch (wall.GetType())
                    {
                        case WallType.Soft:
                            SoftWallEffect(hit.GO);
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
                    d.TakeDamage(1);

            }
        }
        #endregion

        #region EffectsOnTargets

        protected virtual void SoftWallEffect(GameObject go)
        {
            go.GetComponent<IDestroyable>()?.DestroyGO();
        }
        protected virtual void CharachterEffect(GameObject go)
        {
            //Debug.Log("hit charachter");
        }
        #endregion

 

        #region Buffs

        public virtual void BuffLength(float length)
        {
            _explosionLength = length;
        }

        public virtual void BuffPierce(int pierce)
        {
            _piercing = pierce;
        }

        #endregion
    }
}