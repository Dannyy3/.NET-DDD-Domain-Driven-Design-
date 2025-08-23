using Wpm.Management.Domain.Interfaces;
using Wpm.Management.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Management.Domain;

public class Pet : Entity
{
    public string Name { get; init; }
    public int Age { get; init; }
    public string Color { get; init; }
    public Weight? Weight { get; private set; }
    public SexOfPet SexOfPet { get; init; }
    public BreedId BreedId { get; init; }
    public WeigthClass WeigthClass { get; private set; }

    public Pet(Guid id, string name, int age, SexOfPet sexOfPet, string color, BreedId breedId)
    {
        Id = id;
        Name = name;
        Age = age;
        SexOfPet = sexOfPet;
        Color = color;
        BreedId = breedId;
    }

    public void SetWeight(Weight weight, IBreedService breedService)
    {
        if (weight == null)
        {
            throw new ArgumentNullException(nameof(weight), "Weight cannot be null.");
        }
        Weight = weight;
        SetWeightClass(breedService);
    }

    private void SetWeightClass(IBreedService breedService)
    {
        var desiredBreed = breedService.GetBreed(BreedId.Value);
        var (from, to) = SexOfPet switch
        {
            SexOfPet.Male => (desiredBreed.MaleIdealWeight.From, desiredBreed.MaleIdealWeight.To),
            SexOfPet.Female => (desiredBreed.FemaleIdealWeight.From, desiredBreed.FemaleIdealWeight.To),
            _ => throw new NotImplementedException()
        };

        WeigthClass = Weight.Value switch
        {
            _ when Weight.Value < from => WeigthClass.Underweight,
            _ when Weight.Value > to => WeigthClass.Overweight,
            _ => WeigthClass.Normal
        };
    }
}

public enum SexOfPet
{
    Male,
    Female,
    Other
}

public enum WeigthClass
{
    Unknown,
    Underweight,
    Normal,
    Overweight,
    Obese
}
