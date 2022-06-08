using UnityEngine;
using System.Threading;
namespace BomberGame
{
    public class WonderingBehaviour : AIBehaviour
    {
        public AIMovement _movement;
        public INodeMap<Vector2> _map;
        private RandomPositionGenerator _positionGenerator;
        private CancellationTokenSource _tokenSource;
        public WonderingBehaviour(AIMovement movement, INodeMap<Vector2> map)
        {
            _movement = movement;
            _map = map;
        }

        public override void StartBehaviour()
        {
            _tokenSource = new CancellationTokenSource();
            _positionGenerator = new RandomPositionGenerator(_map);
            MoveToRandomPosition(_movement.CurrentPosition);
            _movement.OnPositionReached += OnMovementEnd;
        }

        public override void StopBehaviour()
        {
            _movement.OnPositionReached -= OnMovementEnd;
            _tokenSource?.Cancel();
        }

        Vector2 start = new Vector2(0, 2);
        int i = 1;
        public void MoveToRandomPosition(Vector2 currentPosition)
        {
            Vector2 position = _positionGenerator.GetPosition(currentPosition);
            _movement.Move(position, _tokenSource.Token);
        }

        public void OnMovementEnd(MoveResult result)
        {
            Vector2 current = result.FinalPosition;
            MoveToRandomPosition(current);
        }

    }

}