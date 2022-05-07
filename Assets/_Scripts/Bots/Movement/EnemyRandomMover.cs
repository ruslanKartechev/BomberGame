using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class EnemyRandomMover : EnemyMover, ISpeedBuffable
    {
        [SerializeField] private PositionChecker _positionChecher;
        private DirectionGenerator _directionGen;

        private Coroutine _randomWalk;
        private bool _isMoving = false;
        [SerializeField] private float _gridSize = 1;
        private float _speedBuff = 1;
        public override void Init(float moveTime)
        {
            _directionGen = new DirectionGenerator();
            _positionChecher._charachter = transform;
            _moveTime = moveTime;

        }

        public override void Enable()
        {
            if (_randomWalk != null) 
                StopCoroutine(_randomWalk);
            _randomWalk = StartCoroutine(RandomWalk());
        }

        public override void Disable()
        {
            if (_randomWalk != null) 
                StopCoroutine(_randomWalk);
        }

        private int _directionMoves = 0;
        private int _directionMax = 4;
        private IEnumerator RandomWalk()
        {
            CurrentDir = _directionGen.GetDirection();
            RaiseDirChange();
            _directionMax = Random.Range(2, 9);
            while (true)
            {
                if (_isMoving == false)
                {
                    bool allow = _positionChecher.CheckPositionPush(CurrentDir, _gridSize, _moveTime).Allow;
                    if (allow == true)
                    {
                        Vector3 moveVector = _gridSize * CurrentDir;
                        StartCoroutine(Snapping(_moveTime / _speedBuff, moveVector));
                        RaiseDirChange();
                        _directionMoves++;
                        if (_directionMoves >= _directionMax)
                        {
                            ChangeDir();
                        }
                    }
                    else if (allow == false)
                    {
                        ChangeDir();
                    }
                }
                yield return null;
            }
        }

        private void ChangeDir()
        {
            CurrentDir = _directionGen.GetDirectionOther(CurrentDir);
            _directionMoves = 0;
            _directionMax = Random.Range(4, 9);
        }


        private IEnumerator Snapping(float time, Vector3 moveVector)
        {
            _isMoving = true;
            float elapsed = 0f;
            Vector3 start = transform.position;
            Vector3 end = start + moveVector;
            PrevPosition = start;
            while (elapsed <= time)
            {
                transform.position = Vector3.Lerp(start, end, elapsed / time);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = end;
            PrevPosition = end;
            _isMoving = false;
        }

        // Looping
        // GetDirection
        // CheckDirection
        // MoveDirection


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