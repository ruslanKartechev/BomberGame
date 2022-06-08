namespace BomberGame
{
    public abstract class ConstantAttributeBuff<T> : BuffBase
    {
        public T AttributeValue;
        public override string GetStringValue()
        {
            return AttributeValue.ToString();
        }

    }
}