using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "ActorsContainer", menuName = "SO/Containers/ActorsContainer")]
    public class ActorsContainer : ScriptableObject
    {
        private List<ActorByID> _activeActorsContainer = new List<ActorByID>();
        private List<ActorByID> _allActorsContainer = new List<ActorByID>();

        public void AddActor(InteractableEntity actor, string id)
        {
            _activeActorsContainer.Add(new ActorByID(id, actor));
            _allActorsContainer.Add(new ActorByID(id, actor));
        }

        public InteractableEntity GetActor(string id)
        {
            InteractableEntity result = _activeActorsContainer.Find(t => t.id == id).ActorEntity;
            return result;
        }

        public void RemoveActor(InteractableEntity actor)
        {
            _activeActorsContainer.RemoveAll(t => t.ActorEntity == actor);
        }

        public List<ActorByID> GetAllActors()
        {
            return _activeActorsContainer;
        }

        public void Clear()
        {
            _activeActorsContainer.Clear();
            _allActorsContainer.Clear();
        }


    }
}
