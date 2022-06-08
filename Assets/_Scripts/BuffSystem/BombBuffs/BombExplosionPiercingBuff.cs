
using UnityEngine;
namespace BomberGame.Bombs
{
    [CreateAssetMenu(fileName = "BombExplosionPiercingBuff", menuName = "SO/Buffs/BombExplosionPiercingBuff", order = 1)]
    public class BombExplosionPiercingBuff : BombBuffBase
    {
        [SerializeField] private int Pierce;

        public override void ApplyToBomb(InteractableEntity bomb)
        {
            try
            {
                var buffable = bomb.GetEntityComponent<IExplosionPiercingBuffable>();
                buffable.BuffPiercingDepth(Pierce);
            }
            catch
            {
                Debug.Log("Cannot apply Pierce buff");
            }

        }

        public override string GetStringValue()
        {
            return Pierce.ToString();
        }
    }
}
