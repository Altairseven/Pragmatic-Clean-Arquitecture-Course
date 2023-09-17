namespace Bookify.Domain.Abstractions;

public interface IEntity {
    void ClearDomainEvents();
    IReadOnlyList<IDomainEvent> GetDomainEvents();
}
