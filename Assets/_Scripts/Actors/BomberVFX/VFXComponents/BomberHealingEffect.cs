
using UnityEngine;
namespace BomberGame
{
    public class BomberHealingEffect : ActorEffectBase, IHealingVFX, IMaxHealthBuffVFX
    {
        [SerializeField] private BomberVFXManager _bomberVFX;

        private void Awake()
        {
            _entity.AddEntityComponent<IHealingVFX>(this);
            _entity.AddEntityComponent<IMaxHealthBuffVFX>(this);
        }

        void IHealingVFX.PlayOnce()
        {
            _bomberVFX.PlayEffect(BomberVFXManager.BomberVFXType.Heal);
        }

        void IMaxHealthBuffVFX.PlayBuff(int maxhealth)
        {
            _bomberVFX.PlayEffect(BomberVFXManager.BomberVFXType.Heal);

        }

        void IMaxHealthBuffVFX.PlayDebuff(int maxhealth)
        {
            _bomberVFX.PlayEffect(BomberVFXManager.BomberVFXType.Heal);

        }
    }
}
