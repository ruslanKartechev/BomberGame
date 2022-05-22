using UnityEngine;
using System;
namespace BomberGame.UI
{
    [CreateAssetMenu(fileName = "HealthUIChanngelSO", menuName = "SO/UI/HealthUIChanngelSO", order = 1)]
    public class HealthUIChanngelSO : ScriptableObject
    {
        public Action<int> Update;
        public Action OnHeal;
        public Action OnDamage;
    
        public void RaiseOnDamage()
        {
            if (OnDamage != null)
            {
                OnDamage.Invoke();
            }
            else
            {
                Debug.Log("On damage action null");
            }
        }
        
        public void RaiseOnHeal()
        {
            if(OnHeal != null)
            {
                OnHeal.Invoke();
            }
            else
            {
                Debug.Log("on health action is null");
            }
        }

        public void RaiseUpdate(int value)
        {
            if(Update != null)
            {
                Update.Invoke(value);
            }
            else
            {
                Debug.Log("Update action is null");
            }
        }


    }
}