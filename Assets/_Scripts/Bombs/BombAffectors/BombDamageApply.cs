namespace BomberGame
{
    [System.Serializable]
    public class BombDamageApply
    {
        public void Apply(IDamagable damagable, int amount, string dealerID)
        {
            damagable.TakeDamage(amount, dealerID);
        }
        
    }



}
