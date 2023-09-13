using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

public record BookingReserverdDomainEvent(Guid bookingId) : IDomainEvent;