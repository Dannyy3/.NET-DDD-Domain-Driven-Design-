namespace Wpm.Management.Domain.ValueObjects
{
    public record WeightRange
    {
        public decimal From { get; init; }
        public decimal To { get; init; }

        public WeightRange(decimal from, decimal to)
        {
            if (from < 0 || to < 0)
            {
                throw new ArgumentException("Weight range cannot be negative.");
            }
            if (from > to)
            {
                throw new ArgumentException("From value cannot be greater than To value.");
            }
            From = from;
            To = to;
        }
    }
}
