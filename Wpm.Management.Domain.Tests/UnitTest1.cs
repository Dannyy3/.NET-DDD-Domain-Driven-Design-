using Wpm.Management.Domain.Service;
using Wpm.Management.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Management.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Pet_debe_ser_igual()
    {
        var guid = Guid.NewGuid();
        var breedId = new BreedId(guid, new FakeBreedService());
        var pet1 = new Pet(guid, "pepe", 8, SexOfPet.Male, "Negro", breedId);
        var pet2 = new Pet(guid, "pepe2", 8, SexOfPet.Male, "Blanco", breedId);

        Assert.True(pet1 == pet2);
    }

    [Fact]
    public void Peso_debe_ser_igual()
    {
        var w1 = new Weight(10);
        var w2 = new Weight(10);

        Assert.True(w1 == w2);
    }

    [Fact]
    public void RangoPeso_Debe_Ser_Igual()
    {
        var w1 = new WeightRange(10.5m, 20.5m);
        var w2 = new WeightRange(10.5m, 20.5m);

        Assert.True(w1 == w2);
    }

    [Fact]
    public void BreedId_Debe_Ser_Valido()
    {
        var breedService = new FakeBreedService();
        var id = breedService.Breeds[0].Id;
        var breedId = new BreedId(id, breedService);
        Assert.NotNull(breedId);
    }

    [Fact]
    public void WeightClass_Debe_Ser_Underweight()
    {
        var breedService = new FakeBreedService();
        var breedId = new BreedId(breedService.Breeds[0].Id, breedService);

        var pet = new Pet(Guid.NewGuid(), "pepe", 10, SexOfPet.Male, "Transparente", breedId);
        pet.SetWeight(8.5m, breedService);
        Assert.True(pet.WeigthClass == WeigthClass.Underweight);
    }

    [Fact]
    public void WeightClass_Debe_Ser_Normal()
    {
        var breedService = new FakeBreedService();
        var breedId = new BreedId(breedService.Breeds[0].Id, breedService);

        var pet = new Pet(Guid.NewGuid(), "pepe", 10, SexOfPet.Male, "Transparente", breedId);
        pet.SetWeight(new Weight(25), breedService);
        Assert.True(pet.WeigthClass == WeigthClass.Normal);
    }
}