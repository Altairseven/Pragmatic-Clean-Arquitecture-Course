using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;

namespace Bookify.Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, FirstName firstName, LastName lastName, Email email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }

    public static User Create(FirstName firstname, LastName lastName, Email email)
    {
        var user = new User(Guid.NewGuid(), firstname, lastName, email);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id)); 
        return user;
    }
}