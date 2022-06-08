namespace BomberGame.Bombs
{

    public struct ExplosionTarget<T>
    {
        public InteractableEntity Entity;
        public float DistanceToTarget;
        public T Position;

        public ExplosionTarget(InteractableEntity entity, float distanceToTarget, T position)
        {
            Entity = entity;
            DistanceToTarget = distanceToTarget;
            Position = position;
        }
    }
}
