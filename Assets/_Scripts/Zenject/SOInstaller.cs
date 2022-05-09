using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BomberGame
{
    public class SOInstaller : MonoInstaller
    {
        [SerializeField] private BombSpriteByID _bombSprites;
        [SerializeField] private BombsPrefabs _bombPrefabs;
        [SerializeField] private BombBuffs _buffs;
        [SerializeField] private BuffSpriteByID _buffSprites;
        [Header("ID: 'PlayerView' ")]
        [Space(10)]
        [SerializeField] private SpriteViewSO _playerView;

        public override void InstallBindings()
        {
            Container.Bind<BombSpriteByID>().FromInstance(_bombSprites).AsSingle();
            Container.Bind<BombsPrefabs>().FromInstance(_bombPrefabs).AsSingle();
            Container.Bind<BombBuffs>().FromInstance(_buffs).AsSingle();
            Container.Bind<BuffSpriteByID>().FromInstance(_buffSprites).AsSingle();
            Container.Bind<SpriteViewSO>().FromInstance(_playerView).WithConcreteId("PlayerView");


        }
    }
}
