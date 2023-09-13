using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Bookings.ReserveBooking;

public record ReserveBookingCommand(
    Guid ApartmentId,
    Guid UserId,
    DateOnly StarDate,
    DateOnly EndDate
) : ICommand<Guid>;