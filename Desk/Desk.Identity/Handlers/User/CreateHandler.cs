using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Desk.Domain;
using Desk.Identity.Handlers.User.Commands;
using Desk.Identity.Handlers.User.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Desk.Identity.Handlers.User;

public class CreateHandler : IRequestHandler<CreateCommand, UserModel>
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

    public async Task<UserModel> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await context.BeginTransactionAsync();

        var entity = await context.Users
                .Persist(_mapper)
                .InsertOrUpdateAsync(command, cancellationToken);

        await context.CommitTransactionAsync();

        command.Id = entity.Id;

        return await _mediator.Send(_mapper.Map<GetCommand>(command), cancellationToken);
    }
}