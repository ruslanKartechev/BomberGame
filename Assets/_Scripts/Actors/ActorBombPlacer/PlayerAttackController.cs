using UnityEngine;
using CommonGame.Controlls;
namespace BomberGame
{
    public class PlayerAttackController
    {
        private InputAttackChannelSO _channel;
        private IBombPlacer _placer;
        private ITileMover<Vector2> _mover;

        public PlayerAttackController(InputAttackChannelSO _attackChannel, IBombPlacer placer, ITileMover<Vector2> mover)
        {
            _channel = _attackChannel;
            _placer = placer;
            _mover = mover;
        }

        public void Enable()
        {
            _channel.Attack += Attack;
        }

        public void Disable()
        {
            _channel.Attack -= Attack;
        }

        public void Attack()
        {
            Vector3 position = _mover.FromPosition();
            _placer.PlaceBomb(position);
        }
    }

}