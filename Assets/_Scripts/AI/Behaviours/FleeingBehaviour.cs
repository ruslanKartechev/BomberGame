using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
namespace BomberGame
{
    public class FleeingBehaviour : AIBehaviour
    {
        private AIMovement _movement;
        private INodeMap<Vector2> _map;
        private Vector2 _fleeFromPos;
        private CancellationTokenSource _tokenSource;
        private int number_of_steps = 3;

        public FleeingBehaviour(AIMovement movement, INodeMap<Vector2> map, Vector2 fleeFromPos)
        {
            _movement = movement;
            _map = map;
            _fleeFromPos = fleeFromPos;
        }

        public override void StartBehaviour()
        {
            var path = GetFleePath();
            _movement.OnPathEnd += OnMovementFinish;
            _tokenSource = new CancellationTokenSource();
            _movement.MoveOnPath(path, _tokenSource.Token);
        }

        public override void Abort()
        {
            _movement.OnPathEnd -= OnMovementFinish;
            _tokenSource?.Cancel();
        }

        private void OnMovementFinish(MoveResult result)
        {
            _movement.OnPathEnd -= OnMovementFinish;
            OnBehaviourFinished?.Invoke();
        }

        private List<Vector2> GetFleePath()
        {
            List<Vector2> path = new List<Vector2>();
            Vector2 currentPos = _movement.CurrentPosition;
            for(int i =0; i<number_of_steps; i++)
            {
                ConsiderOptions(ref currentPos, path);
            }
            return path;
        }

        private void ConsiderOptions(ref Vector2 currentPos, List<Vector2> pathList)
        {
            var options = NextStepOptions(currentPos);
            if (options.Count < 0)
                return;
            try
            {
                var farthers = GetFarthestPosition(options);
                if (pathList.Contains(farthers) == false)
                {
                    currentPos = farthers;
                    pathList.Add(currentPos);
                }
            }
            catch { }
        }

        private List<Vector2> NextStepOptions(Vector2 currentPos)
        {
            List<Vector2> options = new List<Vector2>();
            var nodes = _map.GetNodes();
            try
            {
                var neighbours = _map.GetNeighbours(nodes[currentPos]);
                foreach(Node<Vector2> node in neighbours)
                {
                    options.Add(node.Value);   
                }
            }
            catch(System.Exception ex)
            {
                Debug.Log($"Caught: {ex.Message}");
            }
            return options;
        }

        private Vector2 GetFarthestPosition(List<Vector2> options)
        {
            Vector2 result = _fleeFromPos;
            float farthestDist = (_fleeFromPos - result).magnitude;
            foreach (Vector2 pos in options)
            {
                float dist = (_fleeFromPos - pos).magnitude;
                if (dist >= farthestDist)
                {
                    farthestDist = dist;
                    result = pos;
                }
            }
            if(result == _fleeFromPos)
            {
                throw new Exception("No option");
            }
            return result;
        }

    }

}