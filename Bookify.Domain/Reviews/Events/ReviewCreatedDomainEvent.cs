using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;