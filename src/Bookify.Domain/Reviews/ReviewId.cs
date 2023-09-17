using Bookify.Domain.Users;

namespace Bookify.Domain.Bookings;

public record ReviewId(Guid Value) {
    public static ReviewId FromValue(Guid Value) => new ReviewId(Value);
    public static ReviewId New() => FromValue(Guid.NewGuid());
}
