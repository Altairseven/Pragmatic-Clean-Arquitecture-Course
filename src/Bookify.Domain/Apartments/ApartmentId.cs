using Bookify.Domain.Users;

namespace Bookify.Domain.Bookings;

public record ApartmentId(Guid Value) {
    public static ApartmentId FromValue(Guid Value) => new ApartmentId(Value);
    public static ApartmentId New() => FromValue(Guid.NewGuid());
}
