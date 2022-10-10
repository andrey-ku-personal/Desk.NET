using Desk.Core.Handlers.Project.Queries;

namespace Desk.Core.Handlers.Project.Commands;

public class GetAllCommandProfile : Profile
{
    public GetAllCommandProfile()
    {
        CreateMap<GetAllCommand, GetAllQuery>();
    }
}