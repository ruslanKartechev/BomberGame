using UnityEngine;
using System.Collections.Generic;
namespace BomberGame.Bombs
{
    public class ExplosionResultAffector<T>
    {
        private IBombActorAffector _actorAffector;
        private IBombObstacleAffector _obstacleAffector;

        public ExplosionResultAffector(IBombActorAffector actorEffect, IBombObstacleAffector obstacleEffect)
        {
            _actorAffector = actorEffect;
            _obstacleAffector = obstacleEffect;
        }

        public void HandleExplosionResult(List<ExplosionTarget<T>> targets)
        {
            foreach(ExplosionTarget<T> target in targets)
            {
                if(target.Entity.EntityKind == EntityKinds.Actor)
                {
                    _actorAffector.AffectActor(target.Entity);
                }
                else if (target.Entity.EntityKind == EntityKinds.Obstacle)
                {
                    _obstacleAffector.AffectObstacle(target.Entity);
                }

            }
        }
    }

}