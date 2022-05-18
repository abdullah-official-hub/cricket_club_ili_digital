using Framework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NewsService.Core.Domain.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsService.Core.Persistance
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }
        public virtual DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().HasQueryFilter(q => q.IsActive == true);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ProcessSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ProcessSave()
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.Entity is Entity))
            {
                var entity = item.Entity as Entity;
                if (item.State == EntityState.Added)
                {
                    AuditAdd(entity);
                    entity.IsActive = true;
                }
                else if (item.State == EntityState.Modified)
                {
                    AuditUpdate(entity);
                    SetCreateFieldsNotModified(entity, item);
                }
                else if (item.State == EntityState.Deleted)
                {
                    item.State = EntityState.Modified;
                    item.Members.ToList().ForEach(_ => _.IsModified = false);
                    AuditUpdate(entity);
                    item.Property(nameof(entity.IsActive)).IsModified = true;
                    entity.IsActive = false;
                }
            }
        }

        private void AuditAdd(Entity entity)
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.DateLastUpdated = DateTime.UtcNow;
        }

        private void AuditUpdate(Entity entity)
        {
            entity.DateLastUpdated = DateTime.UtcNow;
        }

        private static void SetCreateFieldsNotModified(Entity entity, EntityEntry item)
        {
            item.Property(nameof(entity.DateCreated)).IsModified = false;
            item.Property(nameof(entity.IsActive)).IsModified = false;
        }
    }
}