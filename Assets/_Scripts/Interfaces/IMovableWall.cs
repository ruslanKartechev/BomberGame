
using UnityEngine;

namespace BomberGame
{
    public interface IMovableWall
    {
        bool Move(Vector3 dir,float distance, float time);
    }
}