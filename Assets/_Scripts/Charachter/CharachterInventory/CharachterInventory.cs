using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public abstract class CharachterInventory : MonoBehaviour
    {
        public abstract string GetBomb();

        public abstract Dictionary<string, int> GetBombBuffs();
    }
}