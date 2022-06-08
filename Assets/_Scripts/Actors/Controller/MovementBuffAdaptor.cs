using UnityEngine;
namespace BomberGame
{
    public class MovementBuffAdaptor : ISpeedBuffable
    {
        //private MoveSettings _moveSettings;
        private Actor2DMapMover _mover;
        private ISpeedBuffVFX _speedBuffVFX;
        public MovementBuffAdaptor(Actor2DMapMover mover, ISpeedBuffVFX speedVFX)
        {
            _mover = mover;
            _speedBuffVFX = speedVFX;
        }

        public void BuffSpeed(float speedModifier)
        {
            Debug.Log($"Buffed speed mod to: {speedModifier}");
            _mover.CurrentSpeedModifier = speedModifier;
            if(speedModifier > 1)
                _speedBuffVFX?.PlayBuff();
            else
                _speedBuffVFX?.PlayDebuff();
        }

        public void RestoreOriginal()
        {
            Debug.Log($"Restored speed mod to original: {1}");
            _mover.CurrentSpeedModifier = 1f;
        }
    }
}