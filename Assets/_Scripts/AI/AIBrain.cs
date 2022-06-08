using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
using CommonGame.RandomGen;
namespace BomberGame
{
    public class AIBrain
    {
        private AIMovement _movement;
        private AIAttackController _bomber;
        private Map _map;
        private AIBehaviour _currentBehaviour;

        public AIBrain(AIMovement movement, AIAttackController bomber, Map map)
        {
            _movement = movement;
            _bomber = bomber;
            _map = map;
            _map.OnBombPlaced += OnBombPlaced;
        }

        public void Enable()
        {
            _currentBehaviour = new WonderingBehaviour(_movement, _map);
            _currentBehaviour.StartBehaviour();
        }
        public void Disable()
        {
            _currentBehaviour?.StopBehaviour();
        }

        private async void OnBombPlaced(Vector2 position)
        {
            //float waitTime = 1f;
            //Debug.Log($"Stopping for {waitTime} sec and retur wondering beh");
            //_currentBehaviour?.StopBehaviour();
            //await Task.Delay((int)(1000*waitTime));
            //_currentBehaviour?.StartBehaviour();


            if (ShouldFlee(position))
            {
                _currentBehaviour?.StopBehaviour();
                var fleeBehaviour = new FleeingBehaviour(_movement, _map, position);
                fleeBehaviour.OnFleePositionReached = OnFleeEnd;
                _currentBehaviour = fleeBehaviour;
                _currentBehaviour.StartBehaviour();
            }
        }

        private bool ShouldFlee(Vector2 bombPosition)
        {
            Vector2 currentPos = _movement.CurrentPosition;
            float distance = (bombPosition - currentPos).magnitude;
            if(distance < 8)
            {
                return true;
            }
            return false;
        }

        private void OnFleeEnd()
        {
            _currentBehaviour.StopBehaviour();
            _currentBehaviour = new WonderingBehaviour(_movement, _map);
            _currentBehaviour.StartBehaviour();
        }
    }


    public class FleeingBehaviour : AIBehaviour
    {
        public Action OnFleePositionReached;

        private AIMovement _movement;
        private INodeMap<Vector2> _map;
        private Vector2 _fleeFromPos;
        private CancellationTokenSource _tokenSource;
        int number_of_steps = 3;

        public FleeingBehaviour(AIMovement movement, INodeMap<Vector2> map, Vector2 fleeFromPos)
        {
            _movement = movement;
            _map = map;
            _fleeFromPos = fleeFromPos;
        }

        public override void StartBehaviour()
        {
            var path = GetFleePath();
            _movement.OnPositionReached += OnMovementFinish;
            _tokenSource = new CancellationTokenSource();
            _movement.Move(path, _tokenSource.Token);
        }

        public override void StopBehaviour()
        {
            _movement.OnPositionReached -= OnMovementFinish;
            _tokenSource?.Cancel();
        }

        private void OnMovementFinish(MoveResult result)
        {
            _movement.OnPositionReached -= OnMovementFinish;
            OnFleePositionReached?.Invoke();
        }

        private List<Vector2> GetFleePath()
        {
            List<Vector2> path = new List<Vector2>();
            Vector2 currentPos = _movement.CurrentPosition;
            for(int i =0; i<number_of_steps; i++)
            {
                try
                {
                    var options = NextStepOptions(currentPos);
                    if (options.Count > 0)
                    {
                        try
                        {
                            var farthers = GetFarthestPosition(options);
                            if (path.Contains(farthers) == false)
                            {
                                currentPos = farthers;
                                path.Add(currentPos);
                            }
                        }
                        catch
                        {

                        }
                        
                    }
                }
                catch(System.Exception ex)
                {
                    Debug.Log($"Caught {ex.Message}");
                }
            }
            //DebugPath(path);
            return path;
        }

        private void DebugPath(List<Vector2> path)
        {
            for(int i =0; i< path.Count; i++)
            {
                Debug.Log($"path {i}: {path[i]}");
            }
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


   

    public class AIAttackController
    {





    }

}