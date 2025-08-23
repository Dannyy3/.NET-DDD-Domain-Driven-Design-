using Wpm.Management.Api.Infrasctructure;
using Wpm.Management.Domain;
using Wpm.Management.Domain.Interfaces;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Api.Application
{
    public class ManagementApplicationService(IBreedService breedService, ManagementDbContext dbContext)
    {
        public async Task Handle(CreatePetCommand commnad)
        {
            var breedId = new BreedId(commnad.BreedId, breedService);
            var newPet = new Pet(commnad.Id, commnad.Name, commnad.Age,
                                 commnad.SexOfPet, commnad.Color, breedId);


            if (commnad == null)
            {
                throw new ArgumentNullException(nameof(commnad), "Command cannot be null.");
            }

            await dbContext.Pets.AddAsync(newPet);
            await dbContext.SaveChangesAsync();

        }

        public async Task Handle(SetWeightCommand command)
        {
            var pet = await dbContext.Pets.FindAsync(command.Id);

            if (pet == null)
            {
                throw new KeyNotFoundException($"Pet with ID {command.Id} not found.");
            }
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), "Command cannot be null.");
            }

            pet.SetWeight(command.Weigtht, breedService);
            dbContext.Pets.Update(pet);
            await dbContext.SaveChangesAsync();
        }

    }
}
