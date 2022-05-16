
using UnityEngine;
using System;
using CommonGame.Controlls;
using System.Threading;
using System.Threading.Tasks;
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


    public class CharachterTileMover : CharachterMoverBase, ISpeedBuffable
    {
        private IPositionValidator _positionValidator;
        private ITransformView2D _view;
        private MoveSettings _settings;

        public Vector3 PrevTilePosition { get; private set; }

        private bool _isMoving = false;
        private float _speedBuff = 1;
        private Vector3 _currentPosition;
        private CancellationTokenSource _moveToken;

        public CharachterTileMover(IPositionValidator validator, ITransformView2D view, MoveSettings startSettings)
        {
            _positionValidator = validator;
            _view = view;
            _settings = startSettings;

            if (_view == null)
            {
                Debug.Log("View is null. Start at 0 position");
                _currentPosition = Vector3.zero;
            }
            else
                _currentPosition = _view.GetPostion();
        }

        public override void Init()
        {

        }

        public override void Move(Vector3 dir)
        {
            if(_isMoving == false)
            {
                float time = _settings.SnapTime / _speedBuff;
                ValidationResult res = _positionValidator.CheckPosition(dir, _currentPosition, _settings.GridSize);
                if (res.Allow)
                {
                    _moveToken?.Cancel();
                    _moveToken = new CancellationTokenSource();
                    Vector3 moveVector = _settings.GridSize * dir;
                    Snapping(time, moveVector, _moveToken);
                }
            }
        }


        private async void Snapping(float time, Vector3 moveVector, CancellationTokenSource token)
        {
            _isMoving = true;
            float elapsed = 0f;
            Vector3 start = _currentPosition;
            Vector3 end = start + moveVector;
            PrevTilePosition = start;
            while (elapsed <= time && token.IsCancellationRequested == false)
            {
                _currentPosition = Vector3.Lerp(start, end, elapsed / time);
                _view.UpdatePosition(_currentPosition);
                elapsed += Time.deltaTime;
                await Task.Yield();
            }
            if(token.IsCancellationRequested == false)
            {
                _currentPosition = end;
                _view.UpdatePosition(_currentPosition);
                _isMoving = false;
            }
        }


        public void BuffSpeed(float multiplier)
        {
            _speedBuff = multiplier;
        }
    }
}