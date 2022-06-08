
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace BomberGame
{
    public class Actor2DMapMover : ActorMoverBase<Vector2>, ITileMover
    {
        protected IPositionValidator _positionValidator;
        protected ITransformView2D _transView;
        protected ISpriteView2D _spriteView;
        protected MoveSettings _settings;
        protected bool _isMoving = false;
        protected float _currentSpeedModifier = 1;
        protected Vector2 _currentPosition;
        protected Vector2 _tilePosition;
        public float CurrentSpeedModifier { get => _currentSpeedModifier; set => _currentSpeedModifier = value; }

        public Actor2DMapMover(IPositionValidator validator, ITransformView2D view, ISpriteView2D spriteView, MoveSettings startSettings)
        {
            _positionValidator = validator;
            _transView = view;
            _settings = startSettings;
            _spriteView = spriteView;
        }

        public Vector3 GetPosition()
        {
            return _tilePosition;
        }

        public void InitStartPosition(Vector2 startPosition)
        {
            _currentPosition = startPosition;
            _tilePosition = startPosition;
            RaiseOnPositionChange(_currentPosition);
            _transView?.UpdatePosition(_currentPosition);
        }

        public async override Task ModeDir(Vector2 dir, CancellationToken token)
        {
            if(_isMoving == false)
            {
                float time = _settings.SnapTime / _currentSpeedModifier;
                Vector2 destination = _currentPosition + dir * _settings.GridSize;
                ValidationResult res = _positionValidator.CheckPosition(destination);
                if (res.Allow)
                {
                    Vector2 moveVector = _settings.GridSize * dir;
                    _spriteView.SetViewDirection(_currentPosition, destination);
                    await MoveToNode(time, _currentPosition + moveVector, token);
                }
            }
        }

        protected async Task MoveToNode(float time, Vector2 end, CancellationToken token)
        {
            if(_currentPosition != _tilePosition)
            {
                float t = GetCorrectedTime(time, (_currentPosition - _tilePosition).magnitude, _settings.GridSize);
                _spriteView.SetViewDirection(_currentPosition, _tilePosition) ;
                await (Moving(t, _tilePosition, token));
            }
            float tt = GetCorrectedTime(time, (_currentPosition - end).magnitude, _settings.GridSize);
            if(tt > 0)
            {
                _spriteView.SetViewDirection(_currentPosition, end);
                await Moving(tt, end, token);
            }
        }

        private async Task Moving(float time, Vector2 end, CancellationToken token)
        {
            _isMoving = true;
            float elapsed = 0f;
            Vector2 start = _currentPosition;
            _tilePosition = end;
            while (elapsed <= time)
            {
                token.ThrowIfCancellationRequested();
                _currentPosition = Vector3.Lerp(start, end, elapsed / time);
                _transView.UpdatePosition(_currentPosition);
                elapsed += Time.deltaTime;
                RaiseOnPositionChange(_currentPosition);
                await Task.Yield();
            }
            token.ThrowIfCancellationRequested();
            _currentPosition = end;
            _transView.UpdatePosition(_currentPosition);
            RaiseOnPositionChange(end);
            _isMoving = false;
        }

      
        private float GetCorrectedTime(float refTime, float moveDistance, float refDistance)
        {
            float correctedTime = refTime * moveDistance / refDistance;
            return correctedTime;
        }

 

    }
}