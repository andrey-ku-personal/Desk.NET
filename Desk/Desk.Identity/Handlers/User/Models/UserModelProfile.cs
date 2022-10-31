using AutoMapper;

namespace Desk.Identity.Handlers.User.Models;

public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<UserModel, Domain.Entities.User>()
            .ReverseMap();
    }
}