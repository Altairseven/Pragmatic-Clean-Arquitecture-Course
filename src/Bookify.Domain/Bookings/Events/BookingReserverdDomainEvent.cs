using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events;

public record BookingReserverdDomainEvent(BookingId bookingId) : IDomainEvent;