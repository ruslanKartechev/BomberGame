using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public interface IObstacleMap<T>
    {
        void AddToMap(T position, InteractableEntity user);
        void RemoveFromMap(InteractableEntity user);
        InteractableEntity GetObstacleAt(T position);
        List<InteractableEntity> GetObstacles();
        float GetGridSize();

        MapBorders GetBorders();

        bool IsOutsideBorders(T position);
    }
}