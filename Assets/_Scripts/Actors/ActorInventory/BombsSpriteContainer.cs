using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombsSpriteContainer", menuName = "SO/Containers/BombsSpriteContainer", order = 1)]
    public class BombsSpriteContainer : ScriptableObject
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