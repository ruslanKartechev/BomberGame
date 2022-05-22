
using UnityEngine;
namespace BomberGame
{
    public class MapPositionValidator : IPositionValidator
    {
        private IObstacleMap _map;
        private MapBorders _mapBorders;
        public MapPositionValidator(IObstacleMap map)
        {
            _map = map;
            _mapBorders = _map.GetBorders() ;
            

        }

        public ValidationResult CheckPosition(Vector2 dir, Vector2 from, float distance)
        {
            ValidationResult result = new ValidationResult();
            Vector2 position = from + dir*distance;
            var obstacles = _map.GetObstacles();
            if (obstacles.ContainsKey(position) == true)
            {
                result.Allow = false;
                result.Blocking = obstacles[position];
                return result;
            }
            else
            {
                result.Allow = CheckBorders(position);
                return result;
            }
        }

        public bool CheckPosition(Vector2 position)
        {
            var obstacles = _map.GetObstacles();
            if (obstacles.ContainsKey(position) == true)
            {
                //Debug.Log($"obstacle at {position}");
                return false;
            }
            else
            {
                bool res = CheckBorders(position);
                return res;
            }
        }


        public bool CheckBorders(Vector2 position)
        {
            if (position.x >= _mapBorders.Right)
                return false;
            if (position.x <= _mapBorders.Left)
                return false;
            if (position.y >= _mapBorders.Up)
                return false;
            if (position.y <= _mapBorders.Down)
                return false;
            return true;

        }

    }
}