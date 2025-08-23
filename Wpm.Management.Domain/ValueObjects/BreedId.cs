using Wpm.Management.Domain.Interfaces;

namespace Wpm.Management.Domain.ValueObjects
{
    public record BreedId
    {
        private readonly IBreedService _breedService;
        public Guid Value { get; init; }

        public BreedId(Guid value, IBreedService breedService)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("BreedId cannot be empty.", nameof(value));
            }

            _breedService = breedService;
            ValidateBreed(value);
            Value = value;
        }
        public static BreedId Create(Guid value)
        {
            return new BreedId(value);
        }

        private BreedId(Guid value)
        {
            Value = value;
        }

        private void ValidateBreed(Guid value)
        {
            if (_breedService.GetBreed(value) == null)
            {
                throw new ArgumentException($"Invalid BreedId: {value}.", nameof(value));
            }
        }
    }
}
