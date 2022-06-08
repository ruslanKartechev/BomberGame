using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace CommonGame.Controlls
{
    public class KeyboardInputManager : ControllManagerBase
    {
        [Inject] private InputMoveChannelSO _inputChannel;
        [Inject] private InputAttackChannelSO _attackChannel;
        private Coroutine _inputCheck;

        public override void DisableControlls()
        {
            if (_inputCheck != null)
                StopCoroutine(_inputCheck);
        }

        public override void EnableControlls()
        {

            if (_inputCheck != null)
                StopCoroutine(_inputCheck);
            _inputCheck = StartCoroutine(InputCheck());
        }

        public override void Init(object settings)
        {
            //
        }

        private IEnumerator InputCheck()
        {
            while (true)
            {

                if (Input.GetKey(KeyCode.W))
                {
                    _inputChannel.RaiseEventUp();
                }
                if (Input.GetKey(KeyCode.D))
                {
                    _inputChannel.RaiseEventRight();
                }
                if (Input.GetKey(KeyCode.S))
                {
                    _inputChannel.RaiseEventDown();
                }
                if (Input.GetKey(KeyCode.A))
                {
                    _inputChannel.RaiseEventLeft();
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _attackChannel.RaiseEventAttack();
                }
                yield return null;
            }
        }

    }
}