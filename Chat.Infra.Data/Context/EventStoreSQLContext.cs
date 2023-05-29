using Chat.Domain.Core.Events;
using Chat.Infra.Data.Configurations.Applicator;
using Microsoft.EntityFrameworkCore;


namespace Chat.Infra.Data.Context
{
    public class EventStoreSqlContext : DbContext
    {
        public EventStoreSqlContext(DbContextOptions<EventStoreSqlContext> options) : base(options) { }

        public DbSet<StoredEvent> StoredEvent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations();

            base.OnModelCreating(modelBuilder);
        }
    }
}