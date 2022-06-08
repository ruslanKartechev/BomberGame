using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using BomberGame.UI;
using UnityEngine;
using System;
namespace BomberGame
{
    public class TestBomberInitializer : BombersInitializer
    {
        public List<ActorSpawnData> _actorsToInit = new List<ActorSpawnData>();
        public ActorsUIManager _actorsUI;

        [SerializeField] private ActorsContainer _actorContainer;
        private List<IBomberActor> _initialized = new List<IBomberActor>();

        public override List<IBomberActor> InitializedBombers { get { return _initialized; } }

        public override async Task InitBombers(CancellationToken token)
        {
            foreach(ActorSpawnData spawnData in _actorsToInit)
            {
                spawnData.Actor.Spawn(spawnData.Settings);
                spawnData.Actor.Init();
                spawnData.Actor.SetIdle();
                _initialized.Add(spawnData.Actor);
                _actorContainer?.AddActor(spawnData.Actor, spawnData.Actor.EntityID);
            }
            RaiseOnInitializer();
            await Task.Yield();
        }


    }
}
