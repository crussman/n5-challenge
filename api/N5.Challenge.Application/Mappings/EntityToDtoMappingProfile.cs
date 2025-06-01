
using AutoMapper;

using N5.Challenge.Application.Dtos;
using N5.Challenge.Domain.Dtos;
using N5.Challenge.Domain.Entities;

namespace N5.Challenge.Application.Mappings;

public class EntityToDtoMappingProfile : Profile
{
    public EntityToDtoMappingProfile()
    {
        CreateMap<Permission, PermissionDto>()
            .ForMember(dest => dest.PermissionTypeDescription, opt => opt.MapFrom(src => src.PermissionType.Description))
            .ReverseMap();

        CreateMap<Permission, PermissionElasticSearchDto>()
            .ForMember(dest => dest.PermissionTypeDescription, opt => opt.MapFrom(src => src.PermissionType.Description))
            .ReverseMap();
    }
}