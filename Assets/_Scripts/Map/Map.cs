using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{

    public interface IGridMap
    {
        Vector2 GetLeftCell(Vector2 position);
        Vector2 GetRightCell(Vector2 position);
        Vector2 GetUpCell (Vector2 position);
        Vector2 GetDownCell(Vector2 position);

    }


    [CreateAssetMenu(fileName ="Map", menuName = "SO/Maps/Map_")]
    public class Map : ScriptableObject, IObstacleMap, IActorsMap, IGridMap
    {
        [SerializeField] private float _gridSize = 2;
        [SerializeField] private MapBorders _borders;

        private Dictionary<Vector2, IAdaptableObstacle> _obstacles = new Dictionary<Vector2, IAdaptableObstacle>();
        private Dictionary<IAdaptableObstacle, Vector2> _obstaclesInverse = new  Dictionary<IAdaptableObstacle, Vector2>();
        private Dictionary<IAdaptableActor, Vector2> _actors = new Dictionary<IAdaptableActor, Vector2>();
        private Dictionary<Vector2, IAdaptableActor> _actorsInvers = new Dictionary<Vector2, IAdaptableActor>();


        #region ObstacleMap
        public Dictionary<Vector2, IAdaptableObstacle> GetObstacles()
        {
            return _obstacles;
        }

        void IObstacleMap.AddToMap(Vector2 position, IAdaptableObstacle user)
        {
            if (_obstaclesInverse.ContainsKey(user) == false)
            {
                _obstaclesInverse.Add(user, position);
                _obstacles.Add(position, user);
            }
            else
            {
                Debug.Log("Obstacle already registered registered");
            }            
        }

        void IObstacleMap.RemoveFromMap(IAdaptableObstacle user)
        {
            if (_obstaclesInverse.ContainsKey(user) == true)
            {
                Vector2 position = _obstaclesInverse[user];
                _obstacles.Remove(position);
                _obstaclesInverse.Remove(user);
            }
            else
            {
                Debug.Log("Cannot find registered obstacle");
            }
        }

        public MapBorders GetBorders()
        {
            return _borders;
        }

        public bool IsOnMap(Vector2 pos)
        {
            if (pos.x >= _borders.Right)
                return false;
            if (pos.x <= _borders.Left)
                return false;
            if (pos.y >= _borders.Up)
                return false;
            if (pos.y <= _borders.Down)
                return false;
            return true;
        }

        public float GetGridSize()
        {
            return _gridSize;
        }
        #endregion


        #region ActorsMap
        Dictionary<IAdaptableActor, Vector2> IActorsMap.GetActors()
        {
            return _actors;
        }

        void IActorsMap.AddToMap(IAdaptableActor user,Vector2 position)
        {
            if (_actors.ContainsKey(user) == false)
            {
                _actors.Add(user, position);
                _actorsInvers.Add(position, user);
            }
        }

        void IActorsMap.RemoveFromMap(IAdaptableActor user)
        {
            if (_actors.ContainsKey(user) == true)
            {
                _actorsInvers.Remove(_actors[user]);
                _actors.Remove(user);
            }
        }

        public void UpdatePosition(IAdaptableActor user, Vector2 position)
        {
            if(_actors.ContainsKey(user) == true)
                _actors[user] = position;
        }

        public Dictionary<Vector2, IAdaptableActor> GetActorsInverse()
        {
            return _actorsInvers;
        }
        #endregion



        #region IGridMap
        public Vector2 GetLeftCell(Vector2 gridPosition)
        {
            gridPosition += Vector2.left * _gridSize;
            if (gridPosition.x < _borders.Left)
                throw new System.Exception("Next point is outside the bounds of the map");
            else
                return gridPosition;
        }

        public Vector2 GetRightCell(Vector2 gridPosition)
        {
            gridPosition += Vector2.right * _gridSize;
            if (gridPosition.x > _borders.Right)
                throw new System.Exception("Next point is outside the bounds of the map");
            else
                return gridPosition;
        }

        public Vector2 GetUpCell(Vector2 gridPosition)
        {
            gridPosition += Vector2.up * _gridSize;
            if (gridPosition.x > _borders.Up)
                throw new System.Exception("Next point is outside the bounds of the map");
            else
                return gridPosition;
        }

        public Vector2 GetDownCell(Vector2 gridPosition)
        {
            gridPosition += Vector2.down * _gridSize;
            if (gridPosition.x < _borders.Down)
                throw new System.Exception("Next point is outside the bounds of the map");
            else
                return gridPosition;
        }
        #endregion

    }
}