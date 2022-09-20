namespace OrderManager.Domain.ValueObjects
{
    public class Quantity : ValueObject
    {
        public Quantity(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));

            }

            Value = value;
        }

        public int Value { get; private set; }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
