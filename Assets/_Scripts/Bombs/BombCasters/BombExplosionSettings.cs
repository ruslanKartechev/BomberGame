using System.Collections.Generic;
namespace BomberGame.Bombs
{
    [System.Serializable]
    public struct BombExplosionSettings<T>
    {
        public List<T> CastDirections;
        public float Duration;
    }
}