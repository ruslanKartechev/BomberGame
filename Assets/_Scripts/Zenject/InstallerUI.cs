using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using BomberGame.UI;
namespace BomberGame
{
    public class InstallerUI : MonoInstaller
    {
        [SerializeField] private BombMenuBase _bombMenu;
        [SerializeField] private UIBlock _healthMenu;
        [SerializeField] private BuffMenuBase _buffMenu;
        public override void InstallBindings()
        {
            Container.Bind<BombMenuBase>().FromInstance(_bombMenu).AsSingle();
            Container.Bind<UIBlock>().FromInstance(_healthMenu).AsSingle();
            Container.Bind<BuffMenuBase>().FromInstance(_buffMenu).AsSingle();

        }
    }
}
