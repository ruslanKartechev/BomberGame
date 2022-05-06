using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "BuffSpriteByID", menuName = "SO/Buffs/BuffSpriteByID", order = 1)]
    public class BuffSpriteByID : ScriptableObject
    {
        public List<SpriteByID> BuffSprites = new List<SpriteByID>();
        public Sprite GetSprite(string id)
        {
            Sprite s = BuffSprites.Find(t => t.ID == id).Sprite;
            if (s == null)
                Debug.Log($"Sprite not found {id} ");
            return s;
            
        }
    }
}