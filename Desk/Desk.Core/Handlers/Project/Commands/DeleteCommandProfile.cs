namespace Desk.Core.Handlers.Project.Commands;

public class DeleteCommandProfile : Profile
{
    public DeleteCommandProfile()
    {
        CreateMap<DeleteCommand, Domain.Entities.Project>();
        CreateMap<DeleteCommand, GetCommand>();
    }
}