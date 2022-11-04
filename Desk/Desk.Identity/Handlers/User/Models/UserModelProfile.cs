using AutoMapper;
using Desk.Identity.Handlers.Token.Commands;
using Desk.Identity.Handlers.User.Queries;

namespace Desk.Identity.Handlers.User.Models;

public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<UserModel, Domain.Entities.User>()
            .ReverseMap();

        CreateMap<UserModel, GetQuery>();
        CreateMap<UserModel, GenerateCommand>();
    }
}