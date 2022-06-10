
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace BomberGame
{
    public class Actor2DMapMover : ActorMoverBase<Vector2>, ITileMover<Vector2>
    {
        protected IPositionValidator _positionValidator;
        protected ITransformView2D _transView;
        protected ISpriteView2D _spriteView;
        protected MoveSettings _settings;
        protected bool _isMoving = false;
        protected float _currentSpeedModifier = 1;
        protected Vector2 _currentPosition;
        protected Vector2 _tileToPos;
        protected Vector2 _tileFromPos;
        public float CurrentSpeedModifier { get => _currentSpeedModifier; set => _currentSpeedModifier = value; }

        public Actor2DMapMover(IPositionValidator validator, ITransformView2D view, ISpriteView2D spriteView, MoveSettings startSettings)
        {
            _positionValidator = validator;
            _transView = view;
            _settings = startSettings;
            _spriteView = spriteView;
        }

        public void InitStartPosition(Vector2 startPosition)
        {
            _currentPosition = startPosition;
            _tileToPos = startPosition;
            _tileFromPos = startPosition;
            //RaiseOnPositionChange(_currentPosition);
            _transView?.UpdatePosition(_currentPosition);
        }

        public async override Task ModeToDir(Vector2 dir, CancellationToken token)
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
                    await MoveToPos(time, _currentPosition + moveVector, token);
                }
            }
        }

        protected async Task MoveToPos(float time, Vector2 end, CancellationToken token)
        {
            if(_currentPosition != _tileToPos)
            {
                float t = GetCorrectedTime(time, (_currentPosition - _tileToPos).magnitude, _settings.GridSize);
                _spriteView.SetViewDirection(_currentPosition, _tileToPos) ;
                await (Moving(t, _tileToPos, token));
            }
            float tt = GetCorrectedTime(time, (_currentPosition - end).magnitude, _settings.GridSize);
            if(tt > 0)
            {
                _spriteView.SetViewDirection(_currentPosition, end);
                await Moving(tt, end, token);
            }
        }

        private async Task Moving(float time, Vector2 endpos, CancellationToken token)
        {
            _isMoving = true;
            float elapsed = 0f;
            Vector2 startpos = _currentPosition;
            _tileToPos = endpos;
            while (elapsed <= time)
            {
                token.ThrowIfCancellationRequested();
                SetPosition(Vector2.Lerp(startpos, endpos, elapsed / time));
                elapsed += Time.deltaTime;
                await Task.Yield();
            }
            token.ThrowIfCancellationRequested();
            ConfirmEndPos(endpos);
        }
        
        private void SetPosition(Vector2 position)
        {
            _currentPosition = position;
            RaiseOnPositionChange(position);
            _transView.UpdatePosition(_currentPosition);
        }

        private void ConfirmEndPos(Vector2 endpos)
        {
            _currentPosition = endpos;
            _tileFromPos = endpos;
            _transView.UpdatePosition(_currentPosition);
            RaiseOnPositionChange(endpos);
            _isMoving = false;
        }

        private float GetCorrectedTime(float refTime, float moveDistance, float refDistance)
        {
            float correctedTime = refTime * moveDistance / refDistance;
            return correctedTime;
        }

        public Vector2 RealTimePosition()
        {
            return _currentPosition;
        }

        public Vector2 ToPosition()
        {
            return _tileToPos;
        }

        public Vector2 FromPosition()
        {
            return _tileFromPos;
        }
    }
}