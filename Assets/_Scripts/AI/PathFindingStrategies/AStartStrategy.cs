using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
namespace BomberGame
{
    public class AStartStrategy : PathFindStrategy
    {
        protected float _gridSize;
        protected Map _map;
        protected MapBorders _borders;
        protected IPositionValidator _positionValidator;
        protected List<Vector2> _path = new List<Vector2>();
        private CancellationTokenSource _pathFindToken;
        public AStartStrategy(Map map,float gridSize, IPositionValidator positionValid)
        {
            _gridSize = gridSize;
            _positionValidator = positionValid;
            _map = map;
        }

        public override async Task<List<Vector2>> GetPath(Vector2 start, Vector2 end)
        {
            _pathFindToken?.Cancel();
            _pathFindToken = new CancellationTokenSource();
            await GetAstar(start, end, _pathFindToken.Token);
            //Debug.Log($"<color=yellow> Mypath count: {_path.Count} </color>");
            return _path;
        }

        public override void Stop()
        {
            _pathFindToken?.Cancel();
        }

        public async Task GetAstar(Vector2 start, Vector2 end, CancellationToken token)
        {
            int i = 0;
            int i_max = 100;
            AStarPathFinder<Vector2> algorithm = new AStarPathFinder<Vector2>();
            algorithm.HeuristicCost = ManhattanCost;
            algorithm.NodeTravelCost = EuclideanCost;
            //algorithm.onAddToClosedList = OnAddToClosedList;
            algorithm.Initialize(_map.GetNodeAt(start), _map.GetNodeAt(end));
            while (i < i_max && token.IsCancellationRequested == false)
            {
                algorithm.Step();
                i++;
                if(algorithm.Status == PathFinderStatus.SUCCESS)
                {
                    //Debug.Log("SUCCESS");
                    _path = BuildPathFromReversed(algorithm.GetReversedPath());
                    break;
                }
                //await Task.Delay((int)(0.1f * 1000));
            }
        }

        private List<Vector2> BuildPathFromReversed(List<Node<Vector2>> nodes)
        {
            List<Vector2> path_forward = new List<Vector2>(nodes.Count);
            for(int i = nodes.Count-1; i>= 0; i--)
            {
                path_forward.Add(nodes[i].Value);
            }
            return path_forward;
        }


        public static float ManhattanCost(Vector2 a, Vector2 b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }

        public static float EuclideanCost(Vector2 a, Vector2 b)
        {
            return (a - b).magnitude;
        }

        public void OnAddToClosedList(AStarPathFinder<Vector2>.PathFinderNode v)
        {
        }
    }
}