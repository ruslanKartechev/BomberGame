using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonGame.Controlls
{
    public class KeyboardInputManager : ControllManagerBase
    {
        [SerializeField] private InputMoveChannelSO _inputChannel;
        [SerializeField] private InputAttackChannelSO _attackChannel;
        public override void DisableControlls()
        {
            throw new System.NotImplementedException();
        }

        public override void EnableControlls()
        {
            throw new System.NotImplementedException();
        }

        public override void Init(object settings)
        {
            throw new System.NotImplementedException();
        }

        private void Update()
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
        }

    }
}