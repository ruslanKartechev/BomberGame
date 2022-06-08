using UnityEngine;
using System.Collections.Generic;
using System;
namespace BomberGame.Bombs
{
    public abstract class Bomb : InteractableEntity
    {
        protected Dictionary<Type, object> _entityComponents = new Dictionary<Type, object>();
        public override T GetEntityComponent<T>() where T : class
        {
            object returnVal;
            if (_entityComponents.TryGetValue(typeof(T), out returnVal) == false)
            {
                Debug.Log($"Cannot return {typeof(T)} actor component");
                return null;
            }
            return (T)returnVal;
        }
        public override void AddEntityComponent<T>(object mObject) where T : class
        {
            if (_entityComponents.ContainsKey(typeof(T)) == false)
                _entityComponents.Add(typeof(T), mObject);
            else
                Debug.Log($"Already have component of type: " + typeof(T));
        }

        public abstract void Init(Vector2 position, string placerID, IObstacleMap<Vector2> obstacleMap, IActorsMap<Vector2> actorsMap);
        public abstract void StartBomb();

    }

}