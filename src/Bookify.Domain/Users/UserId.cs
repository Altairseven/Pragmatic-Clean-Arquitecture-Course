using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users;

public record UserId(Guid Value) {
    public static UserId FromValue(Guid Value) => new UserId(Value);
    public static UserId New() => FromValue(Guid.NewGuid());
}