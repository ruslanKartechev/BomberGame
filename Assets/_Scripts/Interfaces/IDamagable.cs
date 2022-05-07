using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public interface IDamagable
    {
        void TakeDamage(int damage, string dealer);
    }

}