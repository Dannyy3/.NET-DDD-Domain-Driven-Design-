using Wpm.Clinic.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Entities;

public class Consultation : AggregateRoot
{
    private readonly List<DrugAdministration> administredDrugs = new();
    private readonly List<VitalSigns> vitalSignsReadings = new();

    public PatientId PatientId { get; init; }
    public Text Diagnosis { get; private set; }
    public Text Treatment { get; private set; }
    public Weight CurrentWeight { get; private set; }
    public ConsultationStatus Status { get; private set; }
    public DateTime Start { get; init; }
    public DateTime? ConsultationEnd { get; private set; }
    public IReadOnlyCollection<DrugAdministration> AdministeredDrugs => administredDrugs.AsReadOnly();
    public IReadOnlyCollection<VitalSigns> VitalSignsReadings => vitalSignsReadings.AsReadOnly();

    public Consultation(PatientId patientId)
    {
        Id = Guid.NewGuid();
        PatientId = patientId;
        Status = ConsultationStatus.Started;
        Start = DateTime.UtcNow;
    }

    public void SetWeight(Weight weight)
    {
        ValidateConsultationStatus();
        ValidateWeight(weight);
        CurrentWeight = weight;
    }

    public void SetDiagnosis(Text diagnosis)
    {
        ValidateConsultationStatus();
        if (diagnosis == null)
        {
            throw new ArgumentNullException(nameof(diagnosis), "Diagnosis cannot be null.");
        }
        Diagnosis = diagnosis;
    }

    public void SetTreatment(Text treatment)
    {
        ValidateConsultationStatus();
        if (treatment == null)
        {
            throw new ArgumentNullException(nameof(treatment), "Treatment cannot be null.");
        }
        Treatment = treatment;
    }

    public void End()
    {
        ValidateConsultationStatus();
        if (Diagnosis == null || Treatment == null || CurrentWeight == null)
        {
            throw new InvalidOperationException("Cannot close consultation without diagnosis, treatment, and weight.");
        }

        Status = ConsultationStatus.Finalized;
        ConsultationEnd = DateTime.UtcNow;
    }

    public void AdministerDrug(DrugId drugId, Dose dose)
    {
        ValidateConsultationStatus();

        var newDrugAdministration = new DrugAdministration(drugId, dose);

        administredDrugs.Add(newDrugAdministration);

    }

    public void RegisterVitalSigns(IEnumerable<VitalSigns> vitalSigns)
    {
        ValidateConsultationStatus();
        if (vitalSigns == null)
        {
            throw new ArgumentNullException(nameof(vitalSigns), "Vital signs cannot be null.");
        }

        vitalSignsReadings.AddRange(vitalSigns);
    }

    private void ValidateWeight(Weight weight)
    {
        if (weight == null)
        {
            throw new ArgumentNullException(nameof(weight), "Weight cannot be null.");
        }
        if (weight.Value <= 0)
        {
            throw new ArgumentException("Weight must be greater than zero.", nameof(weight));
        }
    }

    private void ValidateConsultationStatus()
    {
        if (Status == ConsultationStatus.Finalized)
        {
            throw new InvalidOperationException("The consultation is finalized");
        }
    }
}

public enum ConsultationStatus
{
    Started,
    Finalized
}