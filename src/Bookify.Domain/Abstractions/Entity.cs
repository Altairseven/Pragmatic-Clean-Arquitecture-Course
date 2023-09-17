namespace Bookify.Domain.Abstractions;

public abstract class Entity<TEntityId> : IEntity {
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(TEntityId id) => Id = id;

    protected Entity() {

    }

    public TEntityId Id { get; init; }

    public IReadOnlyList<IDomainEvent> GetDomainEvents() {
        return _domainEvents.ToList();
    }

    public void ClearDomainEvents() {
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent ev) {
        _domainEvents.Add(ev);
    }
}