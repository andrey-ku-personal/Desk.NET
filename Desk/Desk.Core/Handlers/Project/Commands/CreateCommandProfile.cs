namespace Desk.Core.Handlers.Project.Commands;

public class CreateCommandProfile : Profile
{
    public CreateCommandProfile()
    {
        CreateMap<CreateCommand, Domain.Entities.Project>();
        CreateMap<CreateCommand, GetCommand>();
    }
}