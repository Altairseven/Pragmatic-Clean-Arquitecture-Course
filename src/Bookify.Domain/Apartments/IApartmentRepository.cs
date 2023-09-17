using Bookify.Domain.Bookings;

namespace Bookify.Domain.Apartments;

public interface IApartmentRepository
{
    Task<Apartment?> GetByIdAsync(ApartmentId id, CancellationToken ct = default);
    
}