namespace Wpm.Clinic.Domain.ValueObjects
{
    public record Text
    {
        public string Value { get; init; }
        public Text(string value)
        {
            ValidateText(value);
            Value = value;
        }

        private void ValidateText(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Text cannot be null or empty.", nameof(value));
            }

            if (value.Length > 500)
            {
                throw new ArgumentException("Text cannot exceed 500 characters.", nameof(value));
            }
        }

        public static implicit operator Text(string value)
        {
            return new Text(value);
        }
    }
}
