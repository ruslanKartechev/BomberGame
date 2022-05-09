using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame.UI
{
    public abstract class HealthMenuBase : MonoBehaviour
    {
        public abstract void OnDamage();
        public abstract void OnHeal();
        public abstract void SetHealth(string health);
    }
}
