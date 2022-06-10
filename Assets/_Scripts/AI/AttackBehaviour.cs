using UnityEngine;
using CommonGame;
using CommonGame.RandomGen;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
namespace BomberGame
{
    public class AttackBehaviour : AIBehaviour
    {
        public int MaxAttackDistance = 3;

        private IAttackController _attackController;
        private INodeMap<Vector2> _nodeMap;
        private IActorsMap<Vector2> _actorMap;
        private AIMovement _movement;
        private List<InteractableEntity> _enemies;
        private BombingStrat _bombingStrat;

        private CancellationTokenSource _tokenSource;

        private List<Vector2> _attackPath;

        public AttackBehaviour(IAttackController attackController, INodeMap<Vector2> nodeMap, IActorsMap<Vector2> actorMap, AIMovement movement, List<InteractableEntity> enemies)
        {
            _attackController = attackController;
            _nodeMap = nodeMap;
            _actorMap = actorMap;
            _movement = movement;
            _enemies = enemies;
      
            _tokenSource = new CancellationTokenSource();
        }

        public override void StartBehaviour()
        {
            var enemyPos = GetEnemyPos();
            if (enemyPos.x == float.NaN)
                return;
            try
            {
                var path = BuildAttackPath(enemyPos);
                _bombingStrat = new BombingStrat_LastNodes(_movement, _attackController, _tokenSource.Token, MoveToSafePos);
                _bombingStrat.Init(path);
                _attackPath = path;
            }
            catch(System.Exception ex)
            {
                Debug.Log($"Caught {ex.Message}");
                OnBehaviourFinished?.Invoke();
            }
        }

        private Vector2 GetEnemyPos()
        {
            var testEnemy = _enemies[0];
            var enemyPos = new Vector2(float.NaN, float.NaN);
            try
            {
                enemyPos = _actorMap.GetActorNodePosition(testEnemy);
                Debug.Log($"Enemy: {testEnemy.EntityID}, EnemyPos: " + enemyPos.ToString());
                return enemyPos;
            }
            catch
            {
                Debug.Log("Cannot get enemy position");
                return enemyPos;
            }
        }

        public override void Abort()
        {
            _movement.OnPathEnd -= OnSafeMovementEnd;
            _bombingStrat?.Stop();
            _tokenSource?.Cancel();
        }

        public List<Vector2> BuildAttackPath(Vector2 enemyPos)
        {
            AttackStart_CrossTargetPath crossStrat = new AttackStart_CrossTargetPath(MaxAttackDistance, _nodeMap);
            List<Vector2> approachPath = crossStrat.GetCrossPath(_movement.CurrentPosition, enemyPos);
            if(approachPath == null)
            {
                AttackStart_GetToClosestPoint closestPointStrat = new AttackStart_GetToClosestPoint(3, _nodeMap);
                approachPath = closestPointStrat.GetPath(_movement.CurrentPosition, enemyPos);
                
            }
            if (approachPath == null)
                throw new System.Exception("Cannot build attack path");
            return approachPath;
        }

        private void MoveToSafePos()
        {
            var mapCopy = new NodeMapCopy(_nodeMap);
            mapCopy.SetUnwalkable(_attackPath);
            RandomPositionGenerator posGen = new RandomPositionGenerator(mapCopy);
            var startpos = _attackPath[_attackPath.Count - 1];
            var randomPos = posGen.GetPositionLimitedScope(startpos,2);
            try
            {
                AStartStrategy a_start = new AStartStrategy(mapCopy);
                var safePath = a_start.GetPathSync(startpos, randomPos);
                
                _movement.OnPathEnd += OnSafeMovementEnd;
                _movement.MoveOnPath(safePath.Nodes, _tokenSource.Token);
            }catch(System.Exception ex)
            {
                Debug.Log($"Caught: {ex.Message} ");
                OnBehaviourFinished?.Invoke();
            }
        }
        
        private void OnSafeMovementEnd(MoveResult res)
        {
            _movement.OnPathEnd -= OnSafeMovementEnd;
            OnBehaviourFinished?.Invoke();
        }
    }


    public abstract class BombingStrat
    {
        protected AIMovement _movement;
        protected IAttackController _attackController;

        protected BombingStrat(AIMovement movement, IAttackController attackController)
        {
            _movement = movement;
            _attackController = attackController;
        }

        public abstract void Init(List<Vector2> movementPath);
        public abstract void Stop();
    }


    public class BombingStrat_LastNodes : BombingStrat
    {
        private int _maxBombCount = 2;
        private CancellationToken _token;
        private Dictionary<int, Action> _actionTable;
        private Action OnFinish;
        private int _moves;

        public BombingStrat_LastNodes(AIMovement movement,  IAttackController attackController,CancellationToken stopToken , Action onFinish) : base(movement, attackController)
        {
            _token = stopToken;
            OnFinish = onFinish;
        }

        public override void Init(List<Vector2> movementPath)
        {
            _moves = 0;
            _actionTable = new Dictionary<int, Action>();
            for(int i = 0; i < _maxBombCount; i++)
            {
                int moveindex = movementPath.Count - i - 1;
                if (moveindex < 0)
                    break;
                _actionTable.Add(movementPath.Count - i - 1, PlaceBomb);
            }
            _movement.OnGridMove += OnMoveMade;
            _movement.OnPathEnd += OnMoveEnd;
            _movement.MoveOnPath(movementPath, _token);
        }

        public override void Stop()
        {
            _movement.OnGridMove -= OnMoveMade;
            _movement.OnPathEnd -= OnMoveEnd;
        }

        private void PlaceBomb()
        {
            _attackController.Attack();
        }

        private void OnMoveMade()
        {
            _moves++;
            if(_actionTable.TryGetValue(_moves, out Action response))
            {
                response.Invoke();
            }
        }

        private void OnMoveEnd(MoveResult res)
        {
            _movement.OnGridMove -= OnMoveMade;
            _movement.OnPathEnd -= OnMoveEnd;
            OnFinish?.Invoke();
        }

    }



}