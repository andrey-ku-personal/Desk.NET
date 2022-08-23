using AutoMapper.QueryableExtensions;
using Desk.Core.Exceptions;
using Desk.Core.Handlers.Project.Commands;
using Desk.Core.Handlers.Project.Models;
using Desk.Core.Handlers.Project.Queries;
using Desk.Core.Queries;
using Desk.Domain;
using Microsoft.EntityFrameworkCore;

namespace Desk.Core.Handlers.Project;

public class GetHandler : IRequestHandler<GetCommand, ProjectModel>
{
    private readonly IDbContextFactory<EntitiesDbContext> _contextFactory;
    private readonly IMapper _mapper;

    public GetHandler(
        IDbContextFactory<EntitiesDbContext> contextFactory,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<ProjectModel> Handle(GetCommand command, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        return await context.Projects
            .ByQuery(_mapper.Map<GetQuery>(command))
            .ProjectTo<ProjectModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken) ??
                throw new NotFoundException($"Project/{command.Id} was not found");
    }
}