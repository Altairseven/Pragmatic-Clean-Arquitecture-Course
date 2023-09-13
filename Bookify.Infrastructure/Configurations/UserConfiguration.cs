using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configuration;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(user => user.Id);
        
        builder.Property(x => x.FirstName)
            .HasMaxLength(200)
            .HasConversion(
                c => c.Value, 
                value => new FirstName(value)
            );
        
        builder.Property(x => x.LastName)
            .HasMaxLength(200)
            .HasConversion(
                c => c.Value, 
                value => new LastName(value)
            );
        
        builder.Property(x => x.Email)
            .HasMaxLength(400)
            .HasConversion(
                c => c.Value, 
                value => new Domain.Users.Email(value)
            );

        builder.HasIndex(u => u.Email).IsUnique();
    }
}