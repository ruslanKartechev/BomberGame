using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombBuffByID", menuName = "SO/BombBuffByID", order = 1)]
    public class BombBuffers : ScriptableObject
    {
        public List<BuffBase> Buffers = new List<BuffBase>();
        public BuffBase GetBuff(string id)
        {
            return Buffers.Find(t => t.ID == id);
        }
    }
}