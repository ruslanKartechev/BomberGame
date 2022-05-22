using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public class BombMapCaster : IBombCaster
    {

        private IActorsMap _actors;
        private IObstacleMap _obstacles;

        public BombMapCaster(IActorsMap actors, IObstacleMap obstacles)
        {
            _actors = actors;
            _obstacles = obstacles;
        }

        public List<IAdaptableObstacle> GetObstacles(Vector2 start, Vector2 dir, float gridSize, float length, int depth)
        {
            List<IAdaptableObstacle> result = new List<IAdaptableObstacle>();
            var obstacles = _obstacles.GetObstacles();
            var borders = _obstacles.GetBorders();
            int pierced = 0;
            for(int i = 1; i <= length && pierced < depth; i++)
            {
                Vector2 position = start + dir * gridSize * i;
                if(CheckBorders(position, borders) == false)
                    continue;

                if (obstacles.ContainsKey(position))
                {
                    result.Add(obstacles[position]);
                    pierced++;
                }
            }
            return result;
        }


        public List<IAdaptableActor> GetActors(Vector2 start, Vector2 dir, float gridSize, float length)
        {

            List<IAdaptableActor> result = new List<IAdaptableActor>();
            var actors = _actors.GetActorsInverse();
            for (int i = 1; i <= length; i++)
            {
                Vector2 position = start + dir * gridSize * i;
                if (actors.ContainsKey(position))
                {
                    result.Add(actors[position]);
                    Debug.Log("added to actor list");
                }
            }
            return result;
        }


        private bool CheckBorders( Vector2 position, MapBorders borders)
        {
            if (position.x >= borders.Right)
                return false;
            if (position.x <= borders.Left)
                return false;
            if (position.y >= borders.Up)
                return false;
            if (position.y <= borders.Down)
                return false;
            return true;
        }

    }
}