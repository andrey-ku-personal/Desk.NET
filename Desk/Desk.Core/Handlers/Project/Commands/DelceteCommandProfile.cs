namespace Desk.Core.Handlers.Project.Commands;

public class DelceteCommandProfile : Profile
{
    public DelceteCommandProfile()
    {
        CreateMap<DeleteCommand, Domain.Entities.Project>();
        CreateMap<DeleteCommand, GetCommand>();
    }
}