using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Users;
using MediatR;

namespace Bookify.Application.Bookings.ReserveBooking;

internal sealed class BookingReservedDomainEventHandler : INotificationHandler<BookingReserverdDomainEvent>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;


    public BookingReservedDomainEventHandler(IBookingRepository bookingRepository, IUserRepository userRepository, IEmailService emailService)
    {
        _bookingRepository = bookingRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(BookingReserverdDomainEvent notification, CancellationToken ct)
    {
        var booking = await _bookingRepository.GetByIdAsync(notification.bookingId, ct);
        if (booking is null)
            return;

        var user = await _userRepository.GetByIdAsync(booking.UserId, ct);
        if (user is null)
            return;

        await _emailService.SendAsync(
            user.Email,
            "Booking Reserved", "You have 10 minutes to confirm booking");

    }
}
