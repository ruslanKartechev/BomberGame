using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public interface IActorsMap<T>
    {
        void AddToMap(InteractableEntity user, T position);
        void RemoveFromMap(InteractableEntity user);
        void UpdatePosition(InteractableEntity user, T position);
        List<InteractableEntity> GetAllActors();
        Dictionary<InteractableEntity,T> GetAllActorsPositions();
        T GetActorNodePosition(InteractableEntity user);
    }
}