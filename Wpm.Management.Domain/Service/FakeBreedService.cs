using Wpm.Management.Domain.Interfaces;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Domain.Service
{
    public class FakeBreedService : IBreedService
    {
        public readonly List<Breed> Breeds = new List<Breed>
        {
            new Breed(Guid.NewGuid(),"Labrador Retriever", new WeightRange(25, 36), new WeightRange(22, 32)),
            new Breed(Guid.NewGuid(),"German Shepherd", new WeightRange(30, 40), new WeightRange(22, 32)),
            new Breed(Guid.NewGuid(),"Golden Retriever", new WeightRange(29, 34), new WeightRange(25, 32)),
            new Breed(Guid.NewGuid(),"Bulldog", new WeightRange(23, 25), new WeightRange(18, 23)),
            new Breed(Guid.NewGuid(),"Beagle", new WeightRange(9, 11), new WeightRange(8, 10))
        };

        public Breed? GetBreed(Guid id)
        {
            var result = Breeds.Find(b => b.Id == id);
            return result ?? throw new ArgumentException($"Breed with ID {id} not found.");
        }
    }
}

