namespace Desk.Core.Handlers.Project.Models;

public class ProjectModelProfile : Profile
{
    public ProjectModelProfile()
    {
        CreateMap<ProjectModel, Domain.Entities.Project>()
            .ReverseMap();
    }
}