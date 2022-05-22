using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class CountAnimationEffectSimple : BombCountDown
    {
        [SerializeField] private Animator _anim;
        public override void OnExplode()
        {
            
        }

        public override void StartCountdown(float time)
        {
            _anim.Play("Countdown");
        }
    }
}