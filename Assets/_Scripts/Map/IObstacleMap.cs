using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public interface IObstacleMap
    {
        Dictionary<Vector2, IAdaptableObstacle> GetObstacles();
        void AddToMap(Vector2 position, IAdaptableObstacle user);
        void RemoveFromMap(IAdaptableObstacle user);
        MapBorders GetBorders();
        bool IsOnMap(Vector2 position);
        float GetGridSize();
    }
}