using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Reviews;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Bookify.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{

    private static readonly JsonSerializerSettings JsonSerializerSettings = new() {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly IPublisher _publisher;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ApplicationDbContext(
        DbContextOptions opt,
        IPublisher publisher, 
        IDateTimeProvider dateTimeProvider
    ) : base(opt) {
        _publisher = publisher;
        this._dateTimeProvider = dateTimeProvider;
    }

    public DbSet<Apartment> Apartments { get; init; }
    public DbSet<User> User { get; init; }
    public DbSet<Booking> Bookings { get; init; }
    public DbSet<Review> Reviews { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        try
        {

            AddDomainEventsAsOutboxMessages();

            var result = await base.SaveChangesAsync(ct);

            /*await PublishDomainEventsAsync();*/

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency Exception occurred.", ex);
        }
    }


    public void AddDomainEventsAsOutboxMessages() {
        var domainEvents = ChangeTracker
           .Entries<IEntity>()
           .Select(entry => entry.Entity)
           .SelectMany(entity => {
               var domEvs = entity.GetDomainEvents();
               entity.ClearDomainEvents();
               return domEvs;
           }).Select(domainEvent => new OutboxMessage(
            Guid.NewGuid(),
            _dateTimeProvider.UtcNow,
            domainEvent.GetType().Name,
            JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)
           )).ToList();


        AddRange(domainEvents);
    }


   /* private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<IEntity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domEvs = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domEvs;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }*/
}