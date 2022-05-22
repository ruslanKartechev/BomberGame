using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BomberGame
{
    public struct MoveResult
    {
        public Vector2 FinalPosition;
        
    }

    public class AIMovement : ITileMover
    {
        public Vector2 CurrentPosition { get => _pathMover.GetPosition(); }
        public event Action<MoveResult> OnPositionReached;

        private CharachterPathMover _pathMover;
        private PathBuilder _pathBuilder;
        private Map _map;

        private CancellationTokenSource _token;
        public AIMovement(CharachterPathMover pathMover, Map map)
        {
            _pathMover = pathMover;
            _map = map;
            _pathBuilder = new PathBuilder(_map, new MapPositionValidator(_map));
        }

        public async void Move(Vector2 targetPosition)
        {
            Stop();
            List<Vector2> path = _pathBuilder.GetPath(CurrentPosition,targetPosition);
            await _pathMover?.MoveOnPath(path);
            MoveResult result = new MoveResult();
            result.FinalPosition = _pathMover.GetPosition();
            OnPositionReached?.Invoke(result);
        }

        public void Stop()
        {
            _pathMover?.Stop();
        }


        public Vector3 GetPosition()
        {
            return _pathMover.GetPosition();
        }

    }

}