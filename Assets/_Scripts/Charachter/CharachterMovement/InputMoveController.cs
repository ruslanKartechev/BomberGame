
using UnityEngine;
using CommonGame.Controlls;
namespace BomberGame
{
    public class InputMoveController
    {

        private CharachterMoverBase _mover;
        private InputMoveChannelSO _channel;
        private ISpriteView _view;
        public InputMoveController(CharachterMoverBase mover, InputMoveChannelSO moveChannel , ISpriteView view)
        {
            _mover = mover;
            _channel = moveChannel;
            _view = view;
        }

        public void EnableMovement()
        {
            _channel.Up += MoveUp;
            _channel.Down += MoveDown;
            _channel.Right += MoveRight;
            _channel.Left += MoveLeft;
        }

        public void DisableMovement()
        {
            _channel.Up -= MoveUp;
            _channel.Down -= MoveDown;
            _channel.Right -= MoveRight;
            _channel.Left -= MoveLeft;
        }


        private void MoveUp()
        {
            _mover?.Move(Vector3.up);
            _view?.SetView('u');
        }

        private void MoveDown()
        {
            _mover?.Move(-Vector3.up);
            _view?.SetView('d');

        }
        private void MoveRight()
        {
            _mover?.Move(Vector3.right);
            _view?.SetView('r');

        }
        private void MoveLeft()
        {
            _mover?.Move(-Vector3.right);
            _view?.SetView('l');

        }
    }
}