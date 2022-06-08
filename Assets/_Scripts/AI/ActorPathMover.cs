using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using System;
namespace BomberGame
{
    public class ActorPathMover : Actor2DMapMover
    {
        public ActorPathMover(IPositionValidator validator, ITransformView2D transView, ISpriteView2D dirView,  MoveSettings startSettings) 
            : base(validator, transView, dirView, startSettings)
        {
        }

        public async Task MoveOnPath(List<Vector2> positions, CancellationToken token)
        {
            foreach (Vector2 pos in positions)
            {
                try
                {
                    SetView(_currentPosition, pos);
                    await MoveTo(pos, token);
                }
                catch /*(System.Exception ex)*/
                {
                    //Debug.Log("<color=red>Caught: " + ex.Message + "</color>");
                    return;
                }
            }
        }

        public async Task MoveTo(Vector2 position, CancellationToken token)
        {
            float time = _settings.SnapTime / _currentSpeedModifier;
            await MoveToNode(time, position, token);
            token.ThrowIfCancellationRequested();
            _currentPosition = position;
        }

        private void SetView(Vector2 current, Vector2 next)
        {
            if(current.x > next.x)
            {
                _spriteView.SetViewDirection('l');
            } else if(current.x < next.x)
            {
                _spriteView.SetViewDirection('r');
            }
            else if(current.y > next.y)
            {
                _spriteView.SetViewDirection('d');
            }
            else if(current.y < next.y)
            {
                _spriteView.SetViewDirection('u');
            }
        }

    }


}