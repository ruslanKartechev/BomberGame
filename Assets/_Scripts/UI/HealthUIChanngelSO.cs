using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame.UI
{
    [CreateAssetMenu(fileName = "HealthUIChanngelSO", menuName = "SO/UI/HealthUIChanngelSO", order = 1)]
    public class HealthUIChanngelSO : ScriptableObject
    {
        public Action<string> SetHealth;
        public Action OnHeal;
        public Action OnDamage;
    
        public void RaiseOnDamage()
        {
            if (OnDamage != null)
            {
                OnDamage.Invoke();
            }
        }
        
        public void RaiseOnHeal()
        {
            if(OnHeal != null)
            {
                OnHeal.Invoke();
            }
        }

        public void RaiseSetHealth(string text)
        {
            if(SetHealth != null)
            {
                SetHealth.Invoke(text);
            }
        }
    }
}