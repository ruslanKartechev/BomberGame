namespace BomberGame

{
    public class HealApply
    {
        public int Amount;
        public string DealerID;

        public HealApply(int amount, string dealerID)
        {
            Amount = amount;
            DealerID = dealerID;
        }

        public void Apply(IDamagable damagable)
        {
            damagable.TakeDamage(Amount, DealerID);
        }
    }



}
