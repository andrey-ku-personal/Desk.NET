using AutoMapper;
using Desk.Identity.Handlers.User.Models;

namespace Desk.Identity.Handlers.Token.Commands;

public class GenerateCommandProfile : Profile
{
    public GenerateCommandProfile()
    {
        CreateMap<GenerateCommand, UserModel>();
    }
}