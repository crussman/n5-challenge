using N5.Challenge.Application.Mappings;
using N5.Challenge.Infrastructure.Mappings;

namespace N5.Challenge.WebApi.Extensions;

public static class AutoMapperExtensions
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(CommandToValueObjectMappingProfile),
            typeof(EntityToDtoMappingProfile),
            typeof(EntityToValueObjectMappingProfile));
    }
}