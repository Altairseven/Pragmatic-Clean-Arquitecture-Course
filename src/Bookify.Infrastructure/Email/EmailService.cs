using Bookify.Application.Abstractions.Email;

namespace Bookify.Infrastructure.Email;

public class EmailService : IEmailService
{
    public Task SendAsync(Domain.Users.Email recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}