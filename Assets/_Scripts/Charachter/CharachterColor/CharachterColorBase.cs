using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public abstract class CharachterColorBase : MonoBehaviour
    {
        public abstract void SetColor(Color32 color);
        public abstract Color32 GetColor();
        public abstract void UpdateColor();

    }
}