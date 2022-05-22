
using UnityEngine;
namespace BomberGame.Health
{
    public class PawnHealth : HealthKeeper
    {
        public override void DisableDamage()
        {
            base.DisableDamage();
            Debug.Log("damaged");
        }
    }
}