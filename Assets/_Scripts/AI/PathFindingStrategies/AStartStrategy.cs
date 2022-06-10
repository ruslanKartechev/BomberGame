using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
namespace BomberGame
{
    public class AStartStrategy : PathFindStrategy<Vector2>
    {
        protected float _gridSize;
        protected INodeMap<Vector2> _map;
        protected MapBorders _borders;
        protected List<Vector2> _path = new List<Vector2>();
        public AStartStrategy(INodeMap<Vector2> map)
        {
            _map = map;
            _gridSize = _map.GetGridSize();
        }

        public override WalkPath<Vector2> GetPathSync(Vector2 start, Vector2 end)
        {
            int it = 0;
            int it_max = 100;
            AStarPathFinder<Vector2> algorithm = new AStarPathFinder<Vector2>();
            algorithm.HeuristicCost = ManhattanCost;
            algorithm.NodeTravelCost = EuclideanCost;
            algorithm.Initialize(_map.GetNodeAt(start), _map.GetNodeAt(end));
            while (it < it_max)
            {
                algorithm.Step();
                it++;
                if (algorithm.Status == PathFinderStatus.SUCCESS)
                {
                    _path = BuildPathFromReversed(algorithm.GetReversedPath());
                    return new WalkPath<Vector2>(_path);
                }
            }
            throw new System.Exception($"Cannot build A* path in {it_max} iterations");
        }

        public override async Task<WalkPath<Vector2>> GetPathAsync(Vector2 start, Vector2 end, CancellationToken token)
        {
            int it = 0;
            int it_max = 100;
            AStarPathFinder<Vector2> algorithm = new AStarPathFinder<Vector2>();
            algorithm.HeuristicCost = ManhattanCost;
            algorithm.NodeTravelCost = EuclideanCost;
            algorithm.Initialize(_map.GetNodeAt(start), _map.GetNodeAt(end));
            while (it < it_max && token.IsCancellationRequested == false)
            {
                algorithm.Step();
                it++;
                if(algorithm.Status == PathFinderStatus.SUCCESS)
                {
                    _path = BuildPathFromReversed(algorithm.GetReversedPath());
                    return new WalkPath<Vector2>(_path);
                }
            }
            throw new System.Exception($"Cannot build A* path in {it_max} iterations");
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