using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;

namespace Bookify.Domain.Users;

public sealed class User : Entity<UserId>
{
    private User()
    {
        
    }

    private User(UserId id, FirstName firstName, LastName lastName, Email email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public string IdentityId { get; private set; } = string.Empty;

    public static User Create(FirstName firstname, LastName lastName, Email email)
    {
        var user = new User(UserId.New(), firstname, lastName, email);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id)); 
        return user;
    }

    public void SetIdentityId(string identityId) {
        IdentityId = identityId;
    }
}