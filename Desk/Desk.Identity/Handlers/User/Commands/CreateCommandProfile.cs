using AutoMapper;
using Desk.Identity.Handlers.User.Models;

namespace Desk.Identity.Handlers.User.Commands;

public class CreateCommandProfile : Profile
{
    public CreateCommandProfile()
    {
        CreateMap<CreateCommand, Domain.Entities.User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom<UserPasswordHashResolver<CreateCommand>>())
            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.LastLoginTime, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<CreateCommand, GetCommand>();
    }
}