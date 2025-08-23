using Microsoft.EntityFrameworkCore;
using Wpm.Clinic.Domain.Entities;
using Wpm.Clinic.Domain.ValueObjects;

namespace Wpm.Clinic.Api.Infrasctructure
{
    public class ClinicDbContext(DbContextOptions options) : DbContext(options)
    {
        // DbSet properties for entities can be added here
        public DbSet<Consultation> Consultations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Consultation>(consultation =>
            {
                consultation.HasKey(x => x.Id);

                consultation.Property(p => p.PatientId)
                            .HasConversion(v => v.Value, v => new PatientId(v));

                consultation.OwnsOne(p => p.Diagnosis);
                consultation.OwnsOne(p => p.Treatment);
                consultation.OwnsOne(p => p.CurrentWeight);


                consultation.OwnsMany(c => c.AdministeredDrugs, a =>
                {
                    a.WithOwner().HasForeignKey("ConsultationId");
                    a.OwnsOne(d => d.DrugId);
                    a.OwnsOne(d => d.Dose);
                });

                consultation.OwnsMany(c => c.VitalSignsReadings, v =>
                {
                    v.WithOwner().HasForeignKey("ConsultationId");
                });
            });
        }

    }

    public static class ClinicDbContextExtensions
    {
        public static void EnsureDbISCreated(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ClinicDbContext>();
            if (context.Database.EnsureCreated())
            {
                Console.WriteLine("ClinicDbContext database created successfully.");
            }
            else
            {
                Console.WriteLine("ClinicDbContext database already exists.");
            }
            context.Database.CloseConnection();
        }
    }
}
