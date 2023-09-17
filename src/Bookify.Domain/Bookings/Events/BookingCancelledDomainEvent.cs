using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

public record BookingCancelledDomainEvent(BookingId bookingId) : IDomainEvent;