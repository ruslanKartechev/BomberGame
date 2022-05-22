
using UnityEngine;
using Zenject;
using CommonGame.Controlls;
using BomberGame.UI;
using CommonGame.Sound;
using CommonGame;
namespace BomberGame
{
    public class SystemsInstaller : MonoInstaller
    {
        [SerializeField] private GameObject SoundSystem;
        [SerializeField] private GameObject ControllsSystem;
        public override void InstallBindings()
        {
            Container.Bind<HealthUIBlock>().AsSingle();
            Container.Bind<BombMenuManager>().AsSingle();
            //Container.Bind<ISoundSystem>().To<SoundSystem>().FromComponentOn(SoundSystem).AsSingle();
            Container.Bind<GameManager>().AsSingle();
            Container.Bind<LevelManager>().AsSingle();
            Container.Bind<IInputSystem>().To<InputSystem>().FromComponentOn(ControllsSystem).AsSingle();

        }
    }
}