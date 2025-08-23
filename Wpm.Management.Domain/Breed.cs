using Wpm.Management.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Management.Domain
{
    public class Breed : Entity
    {
        public string Name { get; init; }
        public WeightRange MaleIdealWeight { get; init; }
        public WeightRange FemaleIdealWeight { get; init; }

        public Breed(Guid id, string name, WeightRange maleIdealWeight, WeightRange femIdWeig)
        {
            Id = id;
            Name = name;
            MaleIdealWeight = maleIdealWeight;
            FemaleIdealWeight = femIdWeig;
        }
    }
}
