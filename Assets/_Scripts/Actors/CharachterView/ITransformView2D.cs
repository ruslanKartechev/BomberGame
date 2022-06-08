
using UnityEngine;
namespace BomberGame
{
    public interface ITransformView2D
    {
        Vector2 GetPostion();
        void UpdatePosition(Vector2 newPos);

    }
}