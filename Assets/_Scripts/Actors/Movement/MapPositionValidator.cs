
using UnityEngine;
namespace BomberGame
{
    public class MapPositionValidator : IPositionValidator
    {
        private IObstacleMap<Vector2> _map;
        private MapBorders _mapBorders;
        public MapPositionValidator(IObstacleMap<Vector2> map)
        {
            _map = map;
            _mapBorders = _map.GetBorders() ;
            

        }

        public ValidationResult CheckPosition(Vector2 position)
        {
            ValidationResult result = new ValidationResult();
            var entry = _map.GetObstacleAt(position);
            if(entry == null)
            {
                result.Allow = !_map.IsOutsideBorders(position); //CheckBorders(position);
                result.Blocking = null;
                return result;
            }
            else
            {
                result.Allow = false;
                result.Blocking = entry;
            }
            return result;
        }

        public bool CheckPositionSimple(Vector2 position)
        {
            var obstacles = _map.GetObstacles();
            var entry = _map.GetObstacleAt(position);
            if (entry == null)
            {
                return !_map.IsOutsideBorders(position);
            }
            else
            {
                return false;
            }
        }
    }
}