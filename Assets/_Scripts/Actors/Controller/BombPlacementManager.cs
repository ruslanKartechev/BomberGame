using UnityEngine;
using BomberGame.Bombs;
namespace BomberGame
{
    public class BombPlacementManager : IBombPlacer
    {
        private IBombGetter _getter;
        private IBombBuffer _buffer;
        private IBombMap<Vector2> _bombMap;
        private IObstacleMap<Vector2> _obstacleMap;
        private IActorsMap<Vector2> _actorMap;
        private string _id;

        public BombPlacementManager(IBombGetter getter, IBombBuffer buffer, IBombMap<Vector2> bombMap, IObstacleMap<Vector2> obstacleMap, IActorsMap<Vector2> actorMap, string id)
        {
            _getter = getter;
            _buffer = buffer;
            _bombMap = bombMap;
            _obstacleMap = obstacleMap;
            _actorMap = actorMap;
            _id = id;
        }

        public void PlaceBomb(Vector2 position)
        {
            try
            {
                Bomb b = _getter.GetBomb();
                b.Init(position, _id, _obstacleMap, _actorMap);
                _buffer.ApplyBuffs(b);
                b.StartBomb();
                _bombMap?.PlaceBomb(position);
            }
            catch { Debug.Log("problem   b0001");  }
        }
        

    }
}