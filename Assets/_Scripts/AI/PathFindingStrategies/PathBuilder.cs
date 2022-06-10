using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Threading;
namespace BomberGame
{
    public class PathBuilder
    {
        protected Map _map;
        protected PathFindStrategy<Vector2> _currentStrat;
        private CancellationTokenSource _tokenSource;
        public PathBuilder(Map map)
        {
            _tokenSource = new CancellationTokenSource();
            _map = map;
        }

        public async Task<List<Vector2>> GetNormalPath(Vector2 start, Vector2 end)
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            _currentStrat = new AStartStrategy(_map);
            List<Vector2> path = _currentStrat.GetPathSync(start, end).Nodes;
            return path;
        }

        


    }
}