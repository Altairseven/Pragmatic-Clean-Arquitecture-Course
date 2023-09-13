namespace Bookify.Domain.Abstractions;

public abstract class Entity
{
   private readonly List<IDomainEvent> _domainEvents = new();
   
   protected Entity(Guid id) => Id = id;
   
   public Guid Id { get; init; }

   public IReadOnlyList<IDomainEvent> GetDomainEvents()
   {
      return _domainEvents.ToList();
   }

   public void ClearDomainEvents()
   {
      _domainEvents.Clear();
   }

   protected void RaiseDomainEvent(IDomainEvent ev)
   {
      _domainEvents.Add(ev);
   }
}