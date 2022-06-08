
using UnityEngine;
namespace BomberGame.Bombs
{
    [CreateAssetMenu(fileName = "BombRangeBuff", menuName = "SO/Buffs/BombRangeBuff", order = 1)]
    public class BombExplosionRangeBuff : BombBuffBase
    {
        [SerializeField] private int MapCellsRange;

        public override void ApplyToBomb(InteractableEntity bomb)
        {
            try
            {
                var buffable = bomb.GetEntityComponent<IExplosionRangeBuffable>();
                buffable.BuffMapRange(MapCellsRange);
            }
            catch
            {
                Debug.Log("Cannot apply ExplosionRange buff");
            }
        }
        public override string GetStringValue()
        {
            return MapCellsRange.ToString();
        }

    }
}
