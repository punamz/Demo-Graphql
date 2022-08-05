
using AutoMapper;
using message_service.Domains.DTO;
using message_service.Domains.Entities;

namespace demo_graphql.Common;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserEntity, UserDTO>();
        CreateMap<UserDTO, UserEntity>();
    }
}


