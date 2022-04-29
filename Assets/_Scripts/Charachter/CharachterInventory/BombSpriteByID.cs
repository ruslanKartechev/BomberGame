using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombUIByID", menuName = "SO/BombUIByID", order = 1)]
    public class BombSpriteByID : ScriptableObject
    {
        public List<SpriteByID> SpriteByID = new List<SpriteByID>();

        public Sprite GetSprite(string id)
        {
            Sprite ui = SpriteByID.Find(t => t.ID == id).Sprite;
            if (ui == null)
                Debug.Log($"Prefab with id: {id} not found");

            return ui;
        }
    }
}