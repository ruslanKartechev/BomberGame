using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public interface IActorsMap
    {
        Dictionary<IAdaptableActor, Vector2> GetActors();
        Dictionary<Vector2, IAdaptableActor> GetActorsInverse();
        void AddToMap(IAdaptableActor user, Vector2 position);
        void RemoveFromMap(IAdaptableActor user);
        void UpdatePosition(IAdaptableActor user, Vector2 position);
    }
}