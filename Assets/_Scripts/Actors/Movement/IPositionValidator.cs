using UnityEngine;

namespace BomberGame
{
    public interface IPositionValidator
    {
        ValidationResult CheckPosition(Vector2 position);
        bool CheckPositionSimple(Vector2 position); 
    }
}