using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{

    [CreateAssetMenu(fileName = "SpriteViewSO", menuName = "SO/SpriteViewSO", order = 1)]
    public class SpriteViewSO : ScriptableObject
    {
        public Sprite Up;
        public Sprite Down;
        public Sprite Right;
        public Sprite Left;



    }
}