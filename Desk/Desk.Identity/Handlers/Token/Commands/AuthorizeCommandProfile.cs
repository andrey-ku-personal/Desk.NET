using AutoMapper;
using Desk.Identity.Handlers.User.Commands;

namespace Desk.Identity.Handlers.Token.Commands;

public class AuthorizeCommandProfile : Profile
{
    public AuthorizeCommandProfile()
    {
        CreateMap<AuthorizeCommand, GetCommand>();
    }
}