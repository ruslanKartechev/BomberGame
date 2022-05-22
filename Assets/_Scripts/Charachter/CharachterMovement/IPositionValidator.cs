using UnityEngine;

namespace BomberGame
{
    public interface IPositionValidator
    {
        ValidationResult CheckPosition(Vector2 dir, Vector2 from, float distance);
        bool CheckPosition(Vector2 position); 
    }
}