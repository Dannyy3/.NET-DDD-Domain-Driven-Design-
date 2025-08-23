namespace Wpm.Clinic.Domain.ValueObjects
{
    public class DrugId
    {
        public Guid Value { get; }
        public DrugId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Drug ID cannot be empty.", nameof(value));
            }
            Value = value;
        }

        public override string ToString() => Value.ToString();
        public override bool Equals(object? obj)
        {
            if (obj is DrugId other)
            {
                return Value.Equals(other.Value);
            }
            return false;
        }
        public override int GetHashCode() => Value.GetHashCode();

        public static implicit operator DrugId(Guid value)
        {
            return new DrugId(value);
        }
    }
}
