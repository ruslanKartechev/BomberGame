using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public interface IBombCaster
    {
        List<IAdaptableObstacle> GetObstacles(Vector2 start, Vector2 dir, float gridSize, float length, int depth);
        List<IAdaptableActor> GetActors(Vector2 start, Vector2 dir, float gridSize, float length);
    }
}