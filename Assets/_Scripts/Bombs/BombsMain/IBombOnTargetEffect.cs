using System.Collections.Generic;

namespace BomberGame
{
    public interface IBombOnTargetEffect
    {
        void ApplyObstacles(List<IAdaptableObstacle> obstacles);
        void ApplyActors(List<IAdaptableActor> actors);

    }
}