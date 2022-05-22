
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace BomberGame
{
    public class CharachterTileMover : CharachterMoverBase, ISpeedBuffable, ITileMover
    {
        protected IPositionValidator _positionValidator;
        protected ITransformView2D _view;
        protected MoveSettings _settings;
        protected bool _isMoving = false;
        protected float _speedBuff = 1;
        protected Vector2 _currentPosition;
        protected Vector2 _tilePosition;

        protected CancellationTokenSource _moveToken;

        public CharachterTileMover(IPositionValidator validator, ITransformView2D view, MoveSettings startSettings, Vector2 startPosition)
        {
            _positionValidator = validator;
            _view = view;
            _settings = startSettings;

            _currentPosition = startPosition;
            _tilePosition = _currentPosition;
            view?.UpdatePosition(_currentPosition);
            
            
        }

        public override void Move(Vector2 dir)
        {
            if(_isMoving == false)
            {
                float time = _settings.SnapTime / _speedBuff;
                ValidationResult res = _positionValidator.CheckPosition(dir, _currentPosition, _settings.GridSize);
                if (res.Allow)
                {
                    _moveToken?.Cancel();
                    _moveToken = new CancellationTokenSource();
                    Vector2 moveVector = _settings.GridSize * dir;
                    Snapping(time, _currentPosition + moveVector, _moveToken);
                }
            }
        }


        protected async Task Snapping(float time, Vector2 end, CancellationTokenSource token)
        {
            _isMoving = true;
            float elapsed = 0f;
            Vector2 start = _currentPosition;
            //Debug.Log($"start pos: {start}, end pos {end};  time {time}");
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
                _tilePosition = _currentPosition;
            }
        }


        public void BuffSpeed(float multiplier)
        {
            _speedBuff = multiplier;
        }

        public Vector3 GetPosition()
        {
            return _tilePosition;
        }
    }
}