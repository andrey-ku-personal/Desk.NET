using AutoMapper.EntityFrameworkCore;
using Desk.Core.Handlers.Project.Commands;
using Desk.Domain;
using Microsoft.EntityFrameworkCore;

namespace Desk.Core.Handlers.Project;

public class DeleteHandler : IRequestHandler<DeleteCommand, Unit>
{
    private readonly IDbContextFactory<EntitiesDbContext> _contextFactory;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DeleteHandler(
        IDbContextFactory<EntitiesDbContext> contextFactory,
        IMediator mediator,
        IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteCommand command, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var model = await _mediator.Send(_mapper.Map<GetCommand>(command), cancellationToken);

        await context.BeginTransactionAsync();

        await context.Projects
            .Persist(_mapper)
            .RemoveAsync(model, cancellationToken);

        await context.CommitTransactionAsync();

        return Unit.Value;
    }
}