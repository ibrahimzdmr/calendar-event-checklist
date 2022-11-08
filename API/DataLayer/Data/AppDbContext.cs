using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        private string? DbPath { get; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()); 
            DbPath = Path.Join(parent?.FullName, "app.db");
            Database.EnsureCreated();
        }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventItemModelBuilder());
            modelBuilder.ApplyConfiguration(new EventItemLogModelBuilder());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            var added = ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Added)
            .Select(t => t.Entity);

            foreach (var inserted in added)
            {
                if (inserted is EntityBase)
                {
                    var entity = (EntityBase)inserted;
                    entity.CreatedDate = DateTime.UtcNow;
                    entity.ModifiedDate = DateTime.UtcNow;
                    entity.Id = Guid.NewGuid();
                }
            }

            var modified = ChangeTracker.Entries()
            .Where(t => t.State == EntityState.Modified)
            .Select(t => t.Entity);

            foreach (var updated in modified)
            {
                if (updated is EntityBase)
                {
                    var entity = (EntityBase)updated;
                    entity.ModifiedDate = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}