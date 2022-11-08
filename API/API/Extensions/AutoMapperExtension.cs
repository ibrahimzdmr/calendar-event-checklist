using AutoMapper;
using DataLayer.Entities;
using DataLayer.Models;

namespace API.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);


            return services;
        }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EventCreateRequest, EventItem>();
        }
    }
}