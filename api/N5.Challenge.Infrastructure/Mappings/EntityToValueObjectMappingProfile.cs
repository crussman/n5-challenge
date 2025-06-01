using AutoMapper;

using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.ValueObjects;

namespace N5.Challenge.Infrastructure.Mappings;

public class EntityToValueObjectMappingProfile : Profile
{
    public EntityToValueObjectMappingProfile()
    {
        CreateMap<Permission, PermissionModificationData>().ReverseMap();
        CreateMap<Permission, PermissionCreationData>().ReverseMap();
    }
}