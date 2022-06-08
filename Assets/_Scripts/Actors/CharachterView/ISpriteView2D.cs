using UnityEngine;
namespace BomberGame
{
    public interface ISpriteView2D
    {
        void SetViewDirection(char dir);
        void SetViewDirection(Vector2 current, Vector2 next);
        Sprite GetCurrentView();
    }
}