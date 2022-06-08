using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace BomberGame
{
    [DefaultExecutionOrder(0)]
    [DisallowMultipleComponent]
    public class Obstacle : InteractableEntity
    {
        [SerializeField] protected ObstacleKinds _type;
        [Inject] protected IObstacleMap<Vector2> _map;
        protected Dictionary<Type, object> _obstacleComponents = new Dictionary<Type, object>();

        private void Start()
        {
            _map.AddToMap(transform.position, this);
        }

        public ObstacleKinds GetObstacleType()
        {
            return _type;
        }

        public override T GetEntityComponent<T>() where T : class
        {
            object returnVal;
            if (_obstacleComponents.TryGetValue(typeof(T), out returnVal) == false)
            {
                return null;
            }
            return (T)returnVal;
        }
        public override void AddEntityComponent<T>(object mObject) where T : class
        {
            if (_obstacleComponents.ContainsKey(typeof(T)) == false)
                _obstacleComponents.Add(typeof(T), mObject);
            else
                Debug.Log($"Already have component of type: " + typeof(T));
        }
    }
}