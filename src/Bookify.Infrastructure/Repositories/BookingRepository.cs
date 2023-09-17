using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Enums;
using Bookify.Domain.Bookings.ValueObjects;
using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;

internal sealed class BookingRepository : Repository<Booking, BookingId>, IBookingRepository
{

    private static readonly BookingStatus[] ActiveBookingStatuses =
    {
        BookingStatus.Reserved,
        BookingStatus.Confirmed,
        BookingStatus.Completed
    };
    
    
    public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(
        Apartment apartment, 
        DateRange duration, 
        CancellationToken ct = default)
    {
        var any = await _db.Bookings.AnyAsync(
            booking => booking.ApartmentId == apartment.Id &&
                       booking.Duration.Start <= duration.End &&
                       booking.Duration.End >= duration.Start &&
                       ActiveBookingStatuses.Contains(booking.Status), ct);
        return any;
    }
}