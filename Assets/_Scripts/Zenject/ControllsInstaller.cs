using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace CommonGame.Controlls
{
    public class ControllsInstaller : MonoInstaller
    {
        [SerializeField] private InputMoveChannelSO _moveChannel;
        [SerializeField] private InputAttackChannelSO _attackChannel;

        public override void InstallBindings()
        {
            Container.Bind<InputMoveChannelSO>().FromInstance(_moveChannel).AsSingle();
            Container.Bind<InputAttackChannelSO>().FromInstance(_attackChannel).AsSingle();


        }
    }
}