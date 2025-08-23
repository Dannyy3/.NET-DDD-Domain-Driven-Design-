namespace Wpm.SharedKernel
{
    public class AggregateRoot : Entity
    {   // AggregateRoot is a special type of Entity that can contain other Entities and Value Objects.
        // It serves as the root of an aggregate, ensuring that all changes to the aggregate are made through it.
        // This class can be extended with additional methods or properties specific to the aggregate's behavior.
    }
}
