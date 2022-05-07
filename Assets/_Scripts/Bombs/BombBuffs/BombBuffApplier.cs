using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class BombBuffApplier : MonoBehaviour, IBuffer
    {
        public BombBuffers _Buffs;

        public void BuffBomb(GameObject target, Dictionary<string,int> _buffs)
        {
            if (_buffs == null)
                return;
            foreach(string id in _buffs.Keys)
            {
                string fullID = id + "_" + _buffs[id];
                _Buffs.GetBuff(fullID).Apply(target);
            }
        }
    }
}