using Microsoft.EntityFrameworkCore;
using Wpm.Management.Domain;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Api.Infrasctructure
{
    public class ManagementDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                // Le indicamos que breedid es nuetra value obnject, le indicamos que cuando se este
                // persisientod de breedid tome el valor de la porpiedad vlue que esta en breedid
                // la primera parte de has vonversion es de donde va a sacr el valor cuando se escriba en bbdd
                // y la segunda parte es de donde va a sacar el valor cuando se lea de la bbdd
                entity.Property(p => p.BreedId).HasConversion(v => v.Value, v => BreedId.Create(v));
                entity.OwnsOne(x => x.Weight);
            });
        }
    }

    public static class ManagementDbContextExtensions
    {
        public static void EnsureDbISCreated(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ManagementDbContext>();

            if (context.Database.EnsureCreated())
            {
                Console.WriteLine("ManagementDbContext database created successfully.");
            }
            else
            {
                Console.WriteLine("ManagementDbContext database already exists.");
            }

            context.Database.CloseConnection();
        }
    }
}

