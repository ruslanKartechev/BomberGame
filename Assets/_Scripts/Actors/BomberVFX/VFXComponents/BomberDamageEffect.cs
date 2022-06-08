
using UnityEngine;
namespace BomberGame
{
    public class BomberDamageEffect : ActorEffectBase, IDamageVFX
    {
        [SerializeField] private BomberVFXManager _bomberVFX;
        [SerializeField] private FlickerDamageEffect _flickerEffect;
        private void Awake()
        {
            _entity.AddEntityComponent<IDamageVFX>(this);    
        }

        public void PlayOnce(int strength)
        {
            _bomberVFX.PlayEffect(BomberVFXManager.BomberVFXType.Damage);
            _flickerEffect.Play();
        }

    }
}
