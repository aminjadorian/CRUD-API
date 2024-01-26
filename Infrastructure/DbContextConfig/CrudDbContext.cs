using Domain.Primitives;
using Domain.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Infrastructure.DbContextConfig
{
    public class CrudDbContext : DbContext
    {
        private readonly IPublisher _publisher;
        public CrudDbContext(DbContextOptions<CrudDbContext> options, IPublisher publisher)
            : base(options)
        {
            Database.EnsureCreated();
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrudDbContext).Assembly);
        }

        public DbSet<User> Users { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }
        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker.Entries<Entity>()
               .Select(e => e.Entity)
               .SelectMany(e =>
               {
                   List<IDomainEvent> events = e.DomainEvents;

                   return events;
               })
            .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}
