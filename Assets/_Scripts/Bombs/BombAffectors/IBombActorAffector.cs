using System.Collections.Generic;

namespace BomberGame
{
    public interface IBombActorAffector
    {
        void AffectActor(InteractableEntity actor);

    }

    public interface IBombObstacleAffector
    {
        void AffectObstacle(InteractableEntity obstacles);

    }
}