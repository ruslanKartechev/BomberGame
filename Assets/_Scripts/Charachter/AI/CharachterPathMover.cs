using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using System;
namespace BomberGame
{
    public class CharachterPathMover : CharachterTileMover
    {
        private CancellationTokenSource _pathMoveToken;
        private ISpriteView _spriteView;
        public CharachterPathMover(IPositionValidator validator, ITransformView2D view, ISpriteView spriteview, MoveSettings startSettings, Vector2 startPosition) : base(validator, view, startSettings, startPosition)
        {
            _view.UpdatePosition(startPosition);
            _spriteView = spriteview;
        }

        public async Task MoveOnPath(List<Vector2> positions)
        {
            _pathMoveToken?.Cancel();
            _pathMoveToken = new CancellationTokenSource();
            foreach (Vector2 pos in positions)
            {
                if (_pathMoveToken.IsCancellationRequested == false)
                {
                    //Debug.Log($"moving {pos}");
                    SetView(_currentPosition, pos);
                    await MoveTo(pos);
                }
            }
        }

        private void SetView(Vector2 current, Vector2 next)
        {
            if(current.x > next.x)
            {
                _spriteView.SetView('l');
            } else if(current.x < next.x)
            {
                _spriteView.SetView('r');
            }
            else if(current.y > next.y)
            {
                _spriteView.SetView('d');

            }
            else if(current.y < next.y)
            {
                _spriteView.SetView('u');

            }

        }


        public async Task MoveTo(Vector2 position)
        {
            float time = _settings.SnapTime / _speedBuff;
            _moveToken?.Cancel();
            _moveToken = new CancellationTokenSource();
            await Snapping(time, position, _moveToken);
            _currentPosition = position;
        }


        public void Stop()
        {
            _pathMoveToken?.Cancel();
        }
    }


}