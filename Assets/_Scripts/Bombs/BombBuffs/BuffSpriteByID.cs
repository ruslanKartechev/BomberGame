using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "BuffSpriteByID", menuName = "SO/Buffs/BuffSpriteByID", order = 1)]
    public class BuffSpriteByID : ScriptableObject
    {
        public List<SpriteByID> BuffSprites = new List<SpriteByID>();

    }
}