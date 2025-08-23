using Wpm.Management.Domain;
using Wpm.Management.Domain.Interfaces;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Api.Infrasctructure
{
    public class BreedService : IBreedService
    {
        public readonly List<Breed> breeds =
            [
                new Breed(Guid.Parse("63f41afd-ec3b-4300-9b84-fc348c866a76"), "Beagle", new WeightRange(10m, 20m), new WeightRange(5m,15m)),
                new Breed(Guid.Parse("b0a7cf23-01ab-421e-883d-7be8cd72aba5"), "Staffordshire Terrier", new WeightRange(10m, 20m), new WeightRange(5m,15m))
            ];

        public Breed? GetBreed(Guid id)
        {
            var result = breeds.Find(b => b.Id == id);
            return result ?? throw new ArgumentException($"Breed with ID {id} not found.");
        }
    }
}
