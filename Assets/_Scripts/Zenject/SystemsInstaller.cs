
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
        [SerializeField] private BuffManager BuffingSystem;
        [Space(10)]
        [SerializeField] private BombParticlesPoolManager _bombParticlePool;
        public override void InstallBindings()
        {
            Container.Bind<IInputSystem>().To<InputSystem>().FromComponentOn(ControllsSystem).AsSingle();
            Container.Bind<BuffManager>().FromInstance(BuffingSystem).AsSingle();
            Container.Bind<IBombParticlesPool>().To<BombParticlesPoolManager>().FromInstance(_bombParticlePool).AsSingle();
        }
    }
}