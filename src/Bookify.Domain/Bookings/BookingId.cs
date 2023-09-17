using Bookify.Domain.Users;

namespace Bookify.Domain.Bookings;

public record BookingId(Guid Value) {
    public static BookingId FromValue(Guid Value) => new BookingId(Value);
    public static BookingId New() => FromValue(Guid.NewGuid());
}
