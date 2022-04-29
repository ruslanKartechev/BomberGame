
using UnityEngine;

namespace BomberGame
{
    public abstract class BombPlacerBase : MonoBehaviour
    {
        public abstract void Enable();
        public abstract void Disable();
            

        public abstract void PlaceBomb();
    }
}