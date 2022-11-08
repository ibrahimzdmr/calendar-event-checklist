using API.Data;
using Application.Interfaces;
using Application.Services;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServiceExtensions(this IServiceCollection services)
        {
            services.AddTransient<IEventService, EventService>();

            return services;
        }
    }
}