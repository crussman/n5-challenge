using AutoMapper;

using N5.Challenge.Application.Commands;
using N5.Challenge.Domain.ValueObjects;

namespace N5.Challenge.Application.Mappings;

public class CommandToValueObjectMappingProfile : Profile
{
    public CommandToValueObjectMappingProfile()
    {
        CreateMap<ModifyPermissionCommand, PermissionModificationData>();
        CreateMap<RequestPermissionCommand, PermissionCreationData>();
    }
}