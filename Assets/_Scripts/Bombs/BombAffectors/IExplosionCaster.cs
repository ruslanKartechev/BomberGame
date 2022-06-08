using System.Collections.Generic;
using UnityEngine;
namespace BomberGame.Bombs
{
    public interface IExplosionCaster<T>
    {
        InteractableEntity CheckObstacleAt(T position);
        List<InteractableEntity> CheckActorsAt(T position);

        //List<InteractableEntity> GetObstacles(Vector2 start, Vector2 dir, float gridSize, float length, int depth);
        //List<InteractableEntity> GetActors(Vector2 start, Vector2 dir, float gridSize, float length);
    }
}