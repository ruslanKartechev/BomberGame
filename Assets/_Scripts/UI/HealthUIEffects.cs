using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame.UI
{

    public class HealthUIEffects : MonoBehaviour, IDamageEffect, IHealEffect
    {
        [SerializeField] private Animator _anim;
        
        public void ExecuteDamage()
        {
            
        }

        public void ExecuteHeal()
        {
            
        }
    }
}
