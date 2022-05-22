using UnityEngine;
using System.Threading;
namespace BomberGame
{
    public class WonderingBehaviour : AIBehaviour
    {
        public AIMovement _movement;
        public IObstacleMap _map;
        private RandomPositionGenerator _positionGenerator;
        private CancellationTokenSource _token;

        public WonderingBehaviour(AIMovement movement, IObstacleMap map)
        {
            _movement = movement;
            _map = map;
        }

        public override void StartBehaviour()
        {
            Debug.Log("started wondering beh");
            _positionGenerator = new RandomPositionGenerator(_map);
            MoveToRandomPosition(_movement.CurrentPosition);
            _movement.OnPositionReached += OnMovementEnd;
        }

        public override void StopBehaviour()
        {
            _movement.Stop();
            _movement.OnPositionReached -= OnMovementEnd;
        }

        Vector2 start = new Vector2(-6, -2);
        int i = 1;
        public void MoveToRandomPosition(Vector2 currentPosition)
        {
            Vector2 position = _positionGenerator.GetPosition(currentPosition);
            Debug.Log($"<color=blue>Current: {currentPosition}, Next random: {position} </color>");
            if (i == 1)
                _movement.Move(start);
            else
                _movement.Move(position);
            i++;

        }

        public void OnMovementEnd(MoveResult result)
        {
            StopBehaviour();
            //Debug.Log("<color=red> NEW MOVE TARGET </color>");
            //Vector2 current = result.FinalPosition;
            //MoveToRandomPosition(current);
        }

    }

}