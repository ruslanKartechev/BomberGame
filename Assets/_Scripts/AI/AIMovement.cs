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

        private ActorPathMover _pathMover;
        private PathBuilder _pathBuilder;
        private Map _map;

        public AIMovement(ActorPathMover pathMover, Map map)
        {
            _pathMover = pathMover;
            _map = map;
            _pathBuilder = new PathBuilder(_map, new MapPositionValidator(_map));
        }

        public async Task Move(Vector2 targetPosition, CancellationToken token)
        {
            List<Vector2> path = await _pathBuilder.GetPath(CurrentPosition,targetPosition);
            await _pathMover?.MoveOnPath(path, token);
            if (token.IsCancellationRequested)
            {
                return;
            }
            MoveResult result = new MoveResult();
            result.FinalPosition = _pathMover.GetPosition();
            OnPositionReached?.Invoke(result);
        }

        public async Task Move(List<Vector2> path, CancellationToken token)
        {
            await _pathMover?.MoveOnPath(path, token);
            if (token.IsCancellationRequested)
            {
                return;
            }
            MoveResult result = new MoveResult();
            result.FinalPosition = _pathMover.GetPosition();
            OnPositionReached?.Invoke(result);
        }

        public Vector3 GetPosition()
        {
            return _pathMover.GetPosition();
        }
    }

}