using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.ValueObjects;
using Bookify.Domain.Users;

namespace Bookify.Application.Bookings.ReserveBooking;

internal sealed class ReserverBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly PricingService _pricingService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;


    public ReserverBookingCommandHandler(
        IUserRepository userRepository, 
        IApartmentRepository apartmentRepository, 
        IBookingRepository bookingRepository, 
        PricingService pricingService, 
        IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _apartmentRepository = apartmentRepository;
        _bookingRepository = bookingRepository;
        _pricingService = pricingService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, ct);
        if (user is null)
            return Result.Failure<Guid>(UserErrors.NotFound);
        
        var apartment = await _apartmentRepository.GetByIdAsnc(request.ApartmentId, ct);
        if (apartment is null)
        {
            return Result.Failure<Guid>(ApartmentErrors.NotFound);
        }

        var duration = DateRange.Create(request.StarDate, request.EndDate);

        if (await _bookingRepository.IsOverlappingAsync(apartment, duration, ct))
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }

        var booking = Booking.Reserve(
            apartment,
            user.Id,
            duration,
            _dateTimeProvider.UtcNow,
            _pricingService
        );

        _bookingRepository.Add(booking);

        await _unitOfWork.SaveChangesAsync(ct);

        return booking.Id;
    }
}





















