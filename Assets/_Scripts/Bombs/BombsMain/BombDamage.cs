using System.Collections.Generic;

namespace BomberGame
{
    [System.Serializable]
    public class BombDamage : IBombOnTargetEffect
    {
        public DamageBombSettigs _settings;
        public string DealerID;
        public void Init(DamageBombSettigs settings)
        {
            _settings = settings;
        }

        public void ApplyActors(List<IAdaptableActor> actors)
        {
            foreach(IAdaptableActor actor in actors)
            {
                actor.Damage(new DamageApply( _settings.Damage, DealerID));
            }
        }

        public void ApplyObstacles(List<IAdaptableObstacle> obstacles)
        {
            foreach (IAdaptableObstacle obstacle in obstacles)
            {
                obstacle.Damage(new DamageApply(_settings.Damage, DealerID));
            }
        }
    }
}