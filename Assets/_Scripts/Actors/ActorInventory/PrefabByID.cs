
using UnityEngine;
using BomberGame.Bombs;
namespace BomberGame
{

    [System.Serializable]
    public class PrefabByID
    {
        public string ID;
        public Bomb _Bomb;
    }


    [System.Serializable]
    public class SpriteByID
    {
        public string ID;
        public Sprite Sprite;
    }
}