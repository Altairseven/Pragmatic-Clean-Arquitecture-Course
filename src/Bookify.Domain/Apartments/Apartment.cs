using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews;

namespace Bookify.Domain.Apartments;

public sealed class Apartment : Entity<ApartmentId>
{

    private Apartment() { 
    
    }
    
    public Apartment(ApartmentId id, Name name, Description description, Address address, Money price, Money cleaningFee) : 
        base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        CleaningFee = cleaningFee;
    }

    public Name Name { get; private set; }
    public Description Description { get; private set; }
    
    public Address Address { get; private set; }

    public Money Price { get; private set; }

    public Money CleaningFee { get; private set; }
    
    public DateTime? LastBookedOnUtc { get; internal set; }
    
    public List<Amenity> Amenities { get; private set; } = new();


    public ICollection<Booking> Bookings { get; private set; } = default;
    public ICollection<Review> Reviews { get; private set; } = default;
}