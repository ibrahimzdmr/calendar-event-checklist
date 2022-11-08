using API.Data;

namespace API.Extensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();

            return services;
        }
    }
}