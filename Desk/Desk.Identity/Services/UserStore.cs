using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Desk.Domain;
using Desk.Identity.Handlers.User.Commands;
using Desk.Identity.Handlers.User.Models;
using Desk.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Desk.Identity.Services;

public class UserStore : 
    IUserStore<UserModel>,
    IUserPasswordStore<UserModel>
{
    private readonly IDbContextFactory<EntitiesDbContext> _contextFactory;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserStore(
        IDbContextFactory<EntitiesDbContext> contextFactory, 
        IMapper mapper, 
        IMediator mediator
    )
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IdentityResult> CreateAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync();

        await db.Users
            .Persist(_mapper)
            .InsertOrUpdateAsync(user, cancellationToken);

        await db.CommitTransactionAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync();

        await db.Users
            .Persist(_mapper)
            .RemoveAsync(user, cancellationToken);

        await db.CommitTransactionAsync();

        return IdentityResult.Success;
    }

    public void Dispose()
    {
    }

    public async Task<UserModel> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        if (userId == null) throw new ArgumentNullException(nameof(userId));
        try
        {
            return await _mediator.Send(new GetCommand { UserId = userId }, cancellationToken);
        }
        catch (NotFoundException)
        {
            return null!;
        }
    }

    public Task<UserModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetNormalizedUserNameAsync(UserModel user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetPasswordHashAsync(UserModel user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetUserIdAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return await Task.FromResult(user.UserId);
    }

    public async Task<string> GetUserNameAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return await Task.FromResult(user.UserId);
    }

    public Task SetUserNameAsync(UserModel user, string userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> UpdateAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);
        await db.BeginTransactionAsync();

        await db.Users
            .Persist(_mapper)
            .InsertOrUpdateAsync(user, cancellationToken);

        await db.CommitTransactionAsync();

        return IdentityResult.Success;
    }

    public Task<bool> HasPasswordAsync(UserModel user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedUserNameAsync(UserModel user, string normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetPasswordHashAsync(UserModel user, string passwordHash, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}