using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;

namespace Bookify.Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(ReviewId ReviewId) : IDomainEvent;