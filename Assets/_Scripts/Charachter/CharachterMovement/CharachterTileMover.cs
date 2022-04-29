using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame
{
    public class CharachterTileMover : CharachterMoverBase
    {
        [SerializeField] private bool _selfInit = true;
        [Space(10)]
        [SerializeField] private TileMoverSettings _settings;
        [SerializeField] private SingleCircleCaster _raycaster;
        public Vector3 PrevTilePosition { get; private set; }
        private bool _isMoving = false;
        private void Start()
        {
            if (_selfInit)
            {
                Init(null);
                EnableMovement();
            }
            PrevTilePosition = transform.position;
        }

        public override void Init(object settings)
        {

        }

        public override void EnableMovement()
        {
            _inputEvents.Up += MoveUp;
            _inputEvents.Down += MoveDown;
            _inputEvents.Right += MoveRight;
            _inputEvents.Left += MoveLeft;
        }

        public override void DisableMovement()
        {
            _inputEvents.Up -= MoveUp;
            _inputEvents.Down -= MoveDown;
            _inputEvents.Right -= MoveRight;
            _inputEvents.Left -= MoveLeft;
        }


        private void MoveUp()
        {
            Move(Vector3.up);
        }

        private void MoveDown()
        {
            Move(-Vector3.up);
        }
        private void MoveRight()
        {
            Move(Vector3.right);
        }
        private void MoveLeft()
        {
            Move(-Vector3.right);
        }
        

        private void Move(Vector3 dir)
        {
            if(_isMoving == false)
            {
                _raycaster.Distance = _settings.GridDistance;
                _raycaster.Raycast(transform.position, dir);
                if (_raycaster._lastHit == false)
                {
                    Vector3 moveVector = _settings.GridDistance * dir;
                    StartCoroutine(Snapping(_settings.GetTime(), moveVector));
                }
            }
        }

        private IEnumerator Snapping(float time, Vector3 moveVector, Action onEnd = null)
        {
            _isMoving = true;
            float elapsed = 0f;
            Vector3 start = transform.position;
            Vector3 end = start + moveVector;
            PrevTilePosition = start;
            while (elapsed <= time)
            {
                transform.position = Vector3.Lerp(start, end, elapsed / time);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = end;
            PrevTilePosition = end;
            _isMoving = false;
            onEnd?.Invoke();
        }

    }
}