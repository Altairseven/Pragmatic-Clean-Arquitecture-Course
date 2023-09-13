namespace Bookify.Domain.Apartments;

public interface IApartmentRepository
{
    Task<Apartment?> GetByIdAsnc(Guid id, CancellationToken ct = default);
    
}