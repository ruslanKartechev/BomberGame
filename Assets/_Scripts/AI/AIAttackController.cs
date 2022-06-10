using UnityEngine;
namespace BomberGame
{
    public class AIAttackController : IAttackController
    {
        private IBombPlacer _placer;
        private ITileMover<Vector2> _mover;

        public AIAttackController(IBombPlacer placer, ITileMover<Vector2> mover)
        {
            _placer = placer;
            _mover = mover;
        }

        public void Attack()
        {
            Vector3 position = _mover.FromPosition();
            _placer.PlaceBomb(position);
        }
    }

}