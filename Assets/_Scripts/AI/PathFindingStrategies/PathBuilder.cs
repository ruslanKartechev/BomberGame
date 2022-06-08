using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
namespace BomberGame
{
    public class PathBuilder
    {
        protected Map _map;
        protected float _gridSize = 1;
        protected MapPositionValidator _positionValidator;
        protected PathFindStrategy _pathStrategy;

        public PathBuilder(Map map, MapPositionValidator positionValidator)
        {
            _map = map;
            _gridSize = _map.GetGridSize();
            _positionValidator = positionValidator;
        }

        public async Task<List<Vector2>> GetPath(Vector2 start, Vector2 end)
        {
            _pathStrategy = new AStartStrategy(_map,_gridSize, _positionValidator);
            List<Vector2> path = await _pathStrategy.GetPath(start, end);
            return path;
        }
        public void Stop()
        {
            _pathStrategy?.Stop();
        }
    }
}