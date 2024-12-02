using BasicCrudApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicCrudApp.Data
{
    public interface ICoreDbContext
    {

        public DbSet<User> Users { get; set; }

        DbSet<T> Set<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


    }
}
