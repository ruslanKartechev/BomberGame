using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public interface IDestroyable
    {
        void DestroyGO();
    }

    public interface IDamagable
    {
        void TakeDamage(int damage);
    }
    public interface IHealable
    {
        void Heal(int health);
    }
}