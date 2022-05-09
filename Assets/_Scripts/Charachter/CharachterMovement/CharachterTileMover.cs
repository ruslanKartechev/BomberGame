using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame
{
    public class CharachterTileMover : CharachterMoverBase, ISpeedBuffable
    {
        [SerializeField] private bool _selfInit = true;
        [SerializeField] private float DebugStartSpeed = 0.15f;
        [Space(10)]
        [SerializeField] private PositionChecker _positionCheck;
        public Vector3 PrevTilePosition { get; private set; }

        private bool _isMoving = false;
        [SerializeField]  private float _gridSize = 1;
        private float _moveTime;
        private float _speedBuff = 1;
        private void Start()
        {
            if (_selfInit)
            {
                Init(DebugStartSpeed);
                EnableMovement();
            }
            PrevTilePosition = transform.position;
        }

        public override void Init(float moveSpeed)
        {
            _moveTime = moveSpeed;
        }

        public override void EnableMovement()
        {
            InputMoveChannel.Up += MoveUp;
            InputMoveChannel.Down += MoveDown;
            InputMoveChannel.Right += MoveRight;
            InputMoveChannel.Left += MoveLeft;
        }

        public override void DisableMovement()
        {
            InputMoveChannel.Up -= MoveUp;
            InputMoveChannel.Down -= MoveDown;
            InputMoveChannel.Right -= MoveRight;
            InputMoveChannel.Left -= MoveLeft;
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
                float time = _moveTime / _speedBuff;
                CasterResult res = _positionCheck.CheckPositionPush(dir, _gridSize, time);
                if (res.Allow)
                {
                    Vector3 moveVector = _gridSize * dir;
                    StartCoroutine(Snapping(time, moveVector));
                }
            }
        }

        private IEnumerator Snapping(float time, Vector3 moveVector)
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
        }

        public void BuffSpeed(float multiplier)
        {
            if (multiplier <= 0)
            {
                Debug.Log("negative speed multiplier");
                return;
            }
            _speedBuff = multiplier;
        }
    }
}