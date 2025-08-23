namespace Wpm.Clinic.Domain.ValueObjects
{
    public class Dose
    {
        public decimal Quantity { get; init; }

        public UnitOfMeasure Unit { get; init; }

        public Dose(decimal quantity, UnitOfMeasure unit)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            }
            Quantity = quantity;
            Unit = unit;
        }
    }
}

public enum UnitOfMeasure
{
    Milligrams,
    Grams,
    Milliliters,
    Liters,
    Units
}