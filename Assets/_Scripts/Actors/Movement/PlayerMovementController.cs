
using UnityEngine;
using CommonGame.Controlls;
using System.Threading;
namespace BomberGame
{
    public class PlayerMovementController
    {
        private ActorMoverBase<Vector2> _mover;
        private InputMoveChannelSO _channel;
        private CancellationTokenSource _tokenSource;

        public PlayerMovementController(ActorMoverBase<Vector2> mover, InputMoveChannelSO moveChannel)
        {
            _mover = mover;
            _channel = moveChannel;
        }

        public void EnableMovement()
        {
            _tokenSource?.Cancel(); 
            _tokenSource = new CancellationTokenSource();
            _channel.Up += () => { Move('u'); };
            _channel.Down += () => { Move('d'); };
            _channel.Right += () => { Move('r'); };
            _channel.Left += () => { Move('l'); };
        }

        public void DisableMovement()
        {
            _tokenSource?.Cancel();
            _channel.Up -= () => { Move('u'); };
            _channel.Down -= () => { Move('d'); };
            _channel.Right -= () => { Move('r'); };
            _channel.Left -= () => { Move('l'); };
        }

        private void Move(char dir)
        {
            switch (dir)
            {
                case 'u':
                    _mover.ModeToDir(Vector3.up, _tokenSource.Token);

                    break;
                case 'd':
                    _mover.ModeToDir(-Vector3.up, _tokenSource.Token);

                    break;
                case 'r':
                    _mover.ModeToDir(Vector3.right, _tokenSource.Token);

                    break;
                case 'l':
                    _mover.ModeToDir(-Vector3.right, _tokenSource.Token);

                    break;
            }
        }

    }
}