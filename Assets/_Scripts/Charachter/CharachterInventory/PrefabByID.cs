
using UnityEngine;

namespace BomberGame
{

    [System.Serializable]
    public class PrefabByID
    {
        public string ID;
        public BombManager Bomb;
    }


    [System.Serializable]
    public class SpriteByID
    {
        public string ID;
        public Sprite Sprite;
    }
}