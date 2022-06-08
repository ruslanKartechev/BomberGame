using UnityEngine;

namespace BomberGame.Bombs
{
    [System.Serializable]
    public class BombDamageComponent : IBombActorAffector, IBombObstacleAffector, IDamageBuffable
    {
        [SerializeField] private BombDamageApply _apply;
        [SerializeField] private DamageBombSettigs _settings;
        private int _curentDamage;
        [HideInInspector] public string DealerID;

        public void Init()
        {
            _curentDamage = _settings.Damage;
        }

        public void AffectActor(InteractableEntity actor)
        {
            IDamagable damagable = actor.GetEntityComponent<IDamagable>();
            if (damagable != null)
                _apply.Apply(damagable, _curentDamage, DealerID);

        }

        public void AffectObstacle(InteractableEntity obstacle)
        {
            IDamagable damagable = obstacle.GetEntityComponent<IDamagable>();
            if (damagable != null)
                _apply.Apply(damagable, _curentDamage, DealerID);
        }

        void IDamageBuffable.BuffDamage(int damage)
        {
            _curentDamage = damage;
            Debug.Log($"Buffed damage to {damage}");

        }

        void IDamageBuffable.RemoveBuff()
        {
        }

        public void Refresh()
        {
            _curentDamage = _settings.Damage;
        }

    }
}