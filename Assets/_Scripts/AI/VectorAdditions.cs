using UnityEngine;
namespace BomberGame
{
    public static class VectorAdditions
    {
        public static Vector2 AllPosVector(this Vector2 v)
        {
            v.x = Mathf.Abs(v.x);
            v.y = Mathf.Abs(v.y);
            return v;

        }
    }

}