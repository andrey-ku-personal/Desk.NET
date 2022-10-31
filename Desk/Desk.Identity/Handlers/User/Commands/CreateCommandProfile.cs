using AutoMapper;
using Desk.Identity.Services;

namespace Desk.Identity.Handlers.User.Commands;

public class CreateCommandProfile : Profile
{
    public CreateCommandProfile()
    {
        CreateMap<CreateCommand, Domain.Entities.User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => CryptExtension.CreateHash(src.Password)))
            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.LastLoginTime, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<CreateCommand, GetCommand>();
    }
}