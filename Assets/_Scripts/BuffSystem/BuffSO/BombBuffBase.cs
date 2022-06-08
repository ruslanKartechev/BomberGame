using UnityEngine;
namespace BomberGame
{
    public abstract class BombBuffBase : BuffBase
    {
        public override void Apply(InteractableEntity actor)
        {
            IBombBuffer buffer = actor.GetEntityComponent<IBombBuffer>();
            buffer.AddBuff(this);
        }
        public override void StopApply(InteractableEntity actor)
        {
            IBombBuffer buffer = actor.GetEntityComponent<IBombBuffer>();
            buffer.RemoveBuff(this);
        }
        public abstract void ApplyToBomb(InteractableEntity bomb);
    }

    

}