
using UnityEngine;
using System.Collections.Generic;
namespace BomberGame
{
    public interface IBuffer
    {
        void BuffBomb(GameObject target, Dictionary<string, int> buffs);
    }
}