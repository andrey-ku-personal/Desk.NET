using AutoMapper;
using Desk.Identity.Handlers.User.Queries;

namespace Desk.Identity.Handlers.User.Commands;

public class GetCommandProfile : Profile
{
    public GetCommandProfile()
    {
        CreateMap<GetCommand, GetQuery>();
    }
}