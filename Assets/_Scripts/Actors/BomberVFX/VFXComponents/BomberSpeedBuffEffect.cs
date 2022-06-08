
using UnityEngine;
namespace BomberGame
{
    public class BomberSpeedBuffEffect : ActorEffectBase, ISpeedBuffVFX
    {
        [SerializeField] private BomberVFXManager _bomberVFX;

        private void Awake()
        {
            _entity.AddEntityComponent<ISpeedBuffVFX>(this);
        }

        public void PlayBuff()
        {
            _bomberVFX.PlayEffect(BomberVFXManager.BomberVFXType.SpeedBuff);
        }

        public void PlayDebuff()
        {
            _bomberVFX.PlayEffect(BomberVFXManager.BomberVFXType.SpeedDebuff);
        }
    }
}
