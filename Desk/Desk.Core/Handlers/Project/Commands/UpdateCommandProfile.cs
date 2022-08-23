namespace Desk.Core.Handlers.Project.Commands;

public class UpdateCommandProfile : Profile
{
    public UpdateCommandProfile()
    {
        CreateMap<UpdateCommand, Domain.Entities.Project>();
    }
}