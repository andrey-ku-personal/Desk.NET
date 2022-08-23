using Desk.Core.Handlers.Project.Queries;

namespace Desk.Core.Handlers.Project.Commands;

public class GetCommandProfile : Profile
{
    public GetCommandProfile()
    {
        CreateMap<GetCommand, GetQuery>();
    }
}