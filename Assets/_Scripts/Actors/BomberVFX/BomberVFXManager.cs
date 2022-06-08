
using UnityEngine;
using System;
namespace BomberGame
{

    public class BomberVFXManager : MonoBehaviour
    {

        [SerializeField] private ParticleSystem _HealingParticles;
        [SerializeField] private ParticleSystem _DamageParticles;
        [Space(10)]
        [SerializeField] private ParticleSystem _SpeedBuff;
        [SerializeField] private ParticleSystem _SpeedDebuff;
        [Space(10)]
        [SerializeField] private ParticleSystem _DamageBuff;
        [SerializeField] private ParticleSystem _DamageDebuff;

        public enum BomberVFXType { Damage,Heal,SpeedBuff, SpeedDebuff, DamageBuff, DamageDebuff }
        public void PlayEffect(BomberVFXType vfxType)
        {
            switch (vfxType)
            {
                case BomberVFXType.Damage:
                    _DamageParticles.Play();
                    break;
                case BomberVFXType.Heal:
                    _HealingParticles.Play();
                    break;
                case BomberVFXType.SpeedBuff:
                    _SpeedBuff.Play();
                    break;
                case BomberVFXType.SpeedDebuff:
                    _SpeedDebuff.Play();
                    break;
                case BomberVFXType.DamageBuff:
                    _DamageBuff.Play();
                    break;
                case BomberVFXType.DamageDebuff:
                    _DamageDebuff.Play();
                    break;
            }
        }


    }
}
