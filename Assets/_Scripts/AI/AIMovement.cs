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

    public class AIMovement
    {
        public Vector2 CurrentPosition { get => _pathMover.ToPosition(); }
        public event Action<MoveResult> OnPathEnd;
        public event Action OnGridMove;


        private ActorPathMover _pathMover;
        private PathBuilder _pathBuilder;
        private Map _map;

        public AIMovement(ActorPathMover pathMover, Map map)
        {
            _pathMover = pathMover;
            _pathMover.OnStep += OnStep;
            _map = map;
            _pathBuilder = new PathBuilder(_map);
        }

        public async Task MoveToPosition(Vector2 targetPosition, CancellationToken token)
        {
            List<Vector2> path = await _pathBuilder.GetNormalPath(CurrentPosition,targetPosition);
            await _pathMover?.MoveOnPath(path, token);
            if (token.IsCancellationRequested)
            {
                return;
            }
            MoveResult result = new MoveResult();
            result.FinalPosition = _pathMover.FromPosition();
            OnPathEnd?.Invoke(result);
        }

        public async Task MoveOnPath(List<Vector2> path, CancellationToken token)
        {
            await _pathMover?.MoveOnPath(path, token);
            if (token.IsCancellationRequested)
            {
                return;
            }
            MoveResult result = new MoveResult();
            result.FinalPosition = _pathMover.FromPosition();
            OnPathEnd?.Invoke(result);
        }

        private void OnStep()
        {
            OnGridMove?.Invoke();
        }

    }

}