using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasicCrudApp.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CoreDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection") ?? "",
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure();
                    });
            });

            services.AddScoped<ICoreDbContext, CoreDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }
    }
}
