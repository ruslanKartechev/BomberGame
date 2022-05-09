using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
namespace CommonGame.Controlls
{
    public class TouchinputManager : ControllManagerBase
    {
        [SerializeField] private bool SelfInit = false;
        [Space(5)]
        [SerializeField] private ProperButton _up;
        [SerializeField] private ProperButton _down;
        [SerializeField] private ProperButton _left;
        [SerializeField] private ProperButton _right;
        [Space(5)]
        [SerializeField] private ProperButton _attack;
        [Inject] private InputMoveChannelSO _moveChannel;
        [Inject] private InputAttackChannelSO _attackChannel;

        private Coroutine _calling;
        private void Awake()
        {
            if (SelfInit == true)
                EnableControlls();
        }

        public override void Init(object settings)
        {

        }

        public override void EnableControlls()
        {
            _up.OnDown += MoveUp;
            _down.OnDown += MoveDown;
            _left.OnDown += MoveLeft;
            _right.OnDown += MoveRight;
            _up.OnUp += Stop;
            _down.OnUp += Stop;
            _left.OnUp += Stop;
            _right.OnUp += Stop;
            _attack.OnDown += Attack;
        }

        public override void DisableControlls()
        {
            _up.OnDown -= MoveUp;
            _down.OnDown -= MoveDown;
            _left.OnDown -= MoveLeft;
            _right.OnDown -= MoveRight;
            _up.OnUp -= Stop;
            _down.OnUp -= Stop;
            _left.OnUp -= Stop;
            _right.OnUp -= Stop;
            _attack.OnDown -= Attack;
        }

        private void MoveUp()
        {
            if (_calling != null)
                StopCoroutine(_calling);
            _calling = StartCoroutine(Moving(_moveChannel.RaiseEventUp));
        }
        private void MoveDown()
        {
            if (_calling != null)
                StopCoroutine(_calling);
            _calling = StartCoroutine(Moving(_moveChannel.RaiseEventDown));
        }
        private void MoveRight()
        {
            if (_calling != null)
                StopCoroutine(_calling);
            _calling = StartCoroutine(Moving(_moveChannel.RaiseEventRight));
        }
        private void MoveLeft()
        {
            if (_calling != null)
                StopCoroutine(_calling);
            _calling = StartCoroutine(Moving(_moveChannel.RaiseEventLeft));
        }

        private void Attack()
        {
            _attackChannel.RaiseEventAttack();
        }

        private void Stop()
        {
            if (_calling != null)
                StopCoroutine(_calling);
        }

        private IEnumerator Moving(Action call)
        {
            while (true)
            {
                call.Invoke();
                yield return null;
            }
        }


    }
}