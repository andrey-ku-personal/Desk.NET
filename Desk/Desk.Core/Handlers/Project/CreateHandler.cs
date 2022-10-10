using AutoMapper.EntityFrameworkCore;
using Desk.Core.Handlers.Project.Commands;
using Desk.Core.Handlers.Project.Models;
using Desk.Domain;
using Microsoft.EntityFrameworkCore;

namespace Desk.Core.Handlers.Project;

public class CreateHandler : IRequestHandler<CreateCommand, ProjectModel>
{
    private readonly IDbContextFactory<EntitiesDbContext> _contextFactory;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateHandler(
        IDbContextFactory<EntitiesDbContext> contextFactory,
        IMediator mediator,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ProjectModel> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await context.BeginTransactionAsync();

        var entity = await context.Projects
            .Persist(_mapper)
            .InsertOrUpdateAsync(command, cancellationToken);

        await context.CommitTransactionAsync();

        command.Id = entity.Id;

        return await _mediator.Send(_mapper.Map<GetCommand>(command), cancellationToken);
    }
}