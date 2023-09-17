using Bookify.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Configurations;
internal class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage> 
{

    public void Configure(EntityTypeBuilder<OutboxMessage> builder) 
    {
        builder.ToTable("outbox_messages");

        builder.HasKey(x => x.Id);
        builder.Property(c => c.Content).HasColumnType("json");






    }
}
