using BasicCrudApp.Domain.Common;
using BasicCrudApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace BasicCrudApp.Data
{
    public class CoreDbContext : DbContext, ICoreDbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "SYSTEM";
                        entry.Entity.CreatedOn = DateTime.Now;
                        entry.Entity.LastModifiedBy = "SYSTEM";
                        entry.Entity.LastModifiedOn = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "SYSTEM";
                        entry.Entity.LastModifiedOn = DateTime.Now;
                        break;
                }
            }
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //core module tables
            builder.Entity<User>()
                   .ToTable("cor_users")
                   .HasKey(x => x.Id);

            base.OnModelCreating(builder);
        }
    }
}

