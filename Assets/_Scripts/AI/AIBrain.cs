using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

using CommonGame.RandomGen;
namespace BomberGame
{

    public class AIBrain
    {
        public AIState State { get; private set; }

        private InteractableEntity _entity;
        private AIMovement _movement;
        private AIAttackController _bomber;

        private Map _map;
        private AIBehaviour _currentBehaviour;
        private CancellationTokenSource _tokenSource;

        public AIBrain(AIMovement movement, AIAttackController bomber, InteractableEntity entity, Map map)
        {
            _entity = entity;
            _movement = movement;
            _bomber = bomber;
            _map = map;
            _map.OnBombPlaced += OnBombPlaced;
            State = AIState.Idle;
        }

        public enum AIState
        {
            Idle, Wondering, Fleeing, Bombing
        }

        public void Enable()
        {
            StartWonderingBehaviour();
            //_tokenSource = new CancellationTokenSource();
            //TestingBeh(_tokenSource.Token);
            //_currentBehaviour = new WonderingBehaviour(_movement, _map);
            //_currentBehaviour.StartBehaviour();
            //UpdateState(AIState.Bombing);
        }

        private async void TestingBeh(CancellationToken token)
        {
            float checkRate = 5f;
            while(token.IsCancellationRequested == false)
            {

                await Task.Delay((int)(checkRate * 1000));
            }
        }

        public void Disable()
        {
            _tokenSource?.Cancel();
            _currentBehaviour?.Abort();
        }

        private void UpdateState(AIState state)
        {
            State = state;
            Debug.Log($"Current State: {State}");
        }

        private void OnBehaviourFinished()
        {
            Debug.Log("Behaviour end");
            StartWonderingBehaviour();
        }

        private void OnBombPlaced(Vector2 position)
        {
            if (ShouldFlee(position))
            {
                StartFleeBeh(position);
            }
        }


        private void StartWonderingBehaviour()
        {
            _currentBehaviour?.Abort();
            _currentBehaviour = new WonderingBehaviour(_movement, _map);
            _currentBehaviour.StartBehaviour();

        }

        private void StartAttackBehabiour()
        {
            _currentBehaviour?.Abort();
            List<InteractableEntity> actors = ((IActorsMap<Vector2>)_map).GetAllActors();
            List<InteractableEntity> enemies = new List<InteractableEntity>();
            enemies = actors.FindAll(t => t != _entity);
            _currentBehaviour = new AttackBehaviour(_bomber, _map, _map, _movement, enemies);
            _currentBehaviour.OnBehaviourFinished = OnBehaviourFinished;
            _currentBehaviour.StartBehaviour();
        }

        private void StartFleeBeh(Vector2 position)
        {
            _currentBehaviour?.Abort();
            _currentBehaviour = new FleeingBehaviour(_movement, _map, position);
            _currentBehaviour.OnBehaviourFinished = OnBehaviourFinished;
            _currentBehaviour.StartBehaviour();
            UpdateState(AIState.Fleeing);
        }

        //DBG
        private bool ShouldFlee(Vector2 bombPosition)
        {

            Vector2 currentPos = _movement.CurrentPosition;
            float distance = (bombPosition - currentPos).magnitude;
            if(distance <= 6)
            {
                return true;
            }
            return false;
        }
    }



}