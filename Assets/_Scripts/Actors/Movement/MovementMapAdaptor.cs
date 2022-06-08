
using UnityEngine;

namespace BomberGame
{
    public class MovementMapAdaptor
    {
        public IActorsMap<Vector2> _map;
        public InteractableEntity _actor;
        public ActorMoverBase<Vector2> _mover;

        public MovementMapAdaptor(IActorsMap<Vector2> map, InteractableEntity actor, ActorMoverBase<Vector2> mover, Vector2 startPositoin)
        {
            _map = map;
            _actor = actor;
            _mover = mover;
            _map.AddToMap(actor, startPositoin);
            _mover.OnPositionChange += OnPositionChange;
        }

        private void OnPositionChange(Vector2 position)
        {
            _map.UpdatePosition(_actor, position);

        }

    }
}