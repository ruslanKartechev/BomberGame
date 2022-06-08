using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using BomberGame.Bombs;
namespace BomberGame
{
    public class SOInstaller : MonoInstaller
    {
        [SerializeField] private BombsSpriteContainer _bombSprites;
        [SerializeField] private BombsPrefabContainer _bombPrefabs;
        [SerializeField] private BuffContainer _buffs;
        [SerializeField] private BuffSpriteContainer _buffSprites;
        [Header("ID: 'PlayerView' ")]
        [SerializeField] private SpriteViewSO _playerView;
        [Space(10)]
        public Map _testMap;
        [Space(10)]
        public BombPoolChannelSO _bombChannel;
        public ActorsContainer _actorsContainer;
        public override void InstallBindings()
        {
            Container.Bind<BombsSpriteContainer>().FromInstance(_bombSprites).AsSingle();
            Container.Bind<BombsPrefabContainer>().FromInstance(_bombPrefabs).AsSingle();
            Container.Bind<BuffContainer>().FromInstance(_buffs).AsSingle();
            Container.Bind<BuffSpriteContainer>().FromInstance(_buffSprites).AsSingle();
            Container.Bind<SpriteViewSO>().FromInstance(_playerView).WithConcreteId("PlayerView");

            _testMap.GenerateMap();
            Container.Bind<IActorsMap<Vector2>>().FromInstance(_testMap).AsSingle();
            Container.Bind<IObstacleMap<Vector2>>().FromInstance(_testMap).AsSingle();

            Container.Bind<BombPoolChannelSO>().FromInstance(_bombChannel).AsSingle();
            _actorsContainer.Clear();
            Container.Bind<ActorsContainer>().FromInstance(_actorsContainer).AsSingle();

        }
    }
}
