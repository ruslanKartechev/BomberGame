using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame.Bombs
{
    public class BombMapCaster<T> : IExplosionCaster<T>
    {
        private IActorsMap<T> _actorMap;
        private IObstacleMap<T> _obstacleMap;
        private float _posError;

        public BombMapCaster(IActorsMap<T> actors, IObstacleMap<T> obstacles, float posError)
        {
            _actorMap = actors;
            _obstacleMap = obstacles;
            _posError = posError;
        }

        public InteractableEntity CheckObstacleAt(T position)
        {
            if (_obstacleMap.IsOutsideBorders(position) == true)
            {
                throw new System.Exception("Out of Map borders");
            }
            var entity = _obstacleMap.GetObstacleAt(position);
            return entity;
        }

        public List<InteractableEntity> CheckActorsAt(T position)
        {
            List<InteractableEntity> result = new List<InteractableEntity>();
            var actorPositions = _actorMap.GetAllActorsPositions();
            foreach(InteractableEntity actor in actorPositions.Keys)
            {
                float distance = DistanceHelper.GetDistance<T>(actorPositions[actor],position);
                if(distance <= _posError)
                {
                    result.Add(actor);
                }
            }
            return result;
        }

        
    }



}
namespace BomberGame
{
    public static class DistanceHelper
    {
        private static Dictionary<Type, int> _typeTable;
        static DistanceHelper()
        {
            _typeTable = new Dictionary<Type, int>();
            _typeTable.Add(typeof(float), 0);
            _typeTable.Add(typeof(Vector2), 1);
            _typeTable.Add(typeof(Vector3), 2);
        }

        public static float GetDistance<T>(T a, T b)
        {
            if(_typeTable.TryGetValue(typeof(T), out int typeCode))
            {
                switch (typeCode)
                {
                    case 0:
                        float val_a = (float)(object)a;
                        float val_b = (float)(object)b;
                        return Mathf.Abs(val_b - val_a);
                    case 1:
                        Vector2 vector2_a = (Vector2)(object)a;
                        Vector2 vector2_b = (Vector2)(object)b;
                        return (vector2_b - vector2_a).magnitude;
                    case 2:
                        Vector3 vector3_a = (Vector3)(object)a;
                        Vector3 vector3_b = (Vector3)(object)b;
                        return (vector3_b - vector3_a).magnitude;
                    default:
                        throw new System.Exception("Cannot calculate distance for type " + typeof(T));
                }
            }
            else
            {
                throw new System.Exception("Cannot calculate distance for type " + typeof(T));
            }
        }

        
    }
}