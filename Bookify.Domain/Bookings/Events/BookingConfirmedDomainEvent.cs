using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

public record BookingConfirmedDomainEvent(Guid bookingId) : IDomainEvent;