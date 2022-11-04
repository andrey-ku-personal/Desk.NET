using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Desk.Domain;
using Desk.Domain.Entities;
using Desk.Identity.Handlers.User.Commands;
using Desk.Identity.Handlers.User.Models;
using Desk.Identity.Handlers.User.Queries;
using Desk.Shared.Exceptions;
using Desk.Shared.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Desk.Identity.Services;

public class UserStore : IUserStore<UserModel>
    , IUserPasswordStore<UserModel>
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

    public async Task<UserModel> FindByIdAsync(string userName, CancellationToken cancellationToken)
    {
        if (userName == null) throw new ArgumentNullException(nameof(userName));
        try
        {
            return await _mediator.Send(new GetCommand { UserNameOrEmail = userName }, cancellationToken);
        }
        catch (NotFoundException)
        {
            return null!;
        }
    }

    public async Task<UserModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(normalizedUserName)) throw new ArgumentNullException(normalizedUserName);

        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var user = await db.Set<User>()
            .AsNoTracking()
            .Where(u => u.NormalizedUserName == normalizedUserName)
            .ProjectTo<UserModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null) throw new NotFoundException($"User '{normalizedUserName}' was not found");

        return user;
    }

    public async Task<string> GetNormalizedUserNameAsync(UserModel user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.UserName.ToUpper());
    }

    public async Task<string> GetUserIdAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await db.Set<User>()
            .AsNoTracking()
            .ByQuery(_mapper.Map<GetQuery>(user))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) throw new NotFoundException($"User '{user.UserName}' was not found");

        return entity.UserName;
    }

    public async Task<string> GetUserNameAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return await Task.FromResult(user.UserName);
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

    #region User Password Store

    public async Task<string> GetPasswordHashAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await db.Set<User>()
            .AsNoTracking()
            .ByQuery(_mapper.Map<GetQuery>(user))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) throw new NotFoundException($"User '{user.UserName}' was not found");

        return entity.PasswordHash;
    }

    public async Task<bool> HasPasswordAsync(UserModel user, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await db.Set<User>()
            .ByQuery(_mapper.Map<GetQuery>(user))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) throw new NotFoundException($"User '{user.UserName}' was not found");

        return !string.IsNullOrWhiteSpace(entity.PasswordHash);
    }

    public async Task SetPasswordHashAsync(UserModel user, string passwordHash, CancellationToken cancellationToken)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        await using var db = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await db.Set<User>()
            .ByQuery(_mapper.Map<GetQuery>(user))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null) throw new NotFoundException($"User '{user.UserName}' was not found");

        entity.PasswordHash = passwordHash;

        await db.CommitTransactionAsync();
    }

    public Task SetNormalizedUserNameAsync(UserModel user, string normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    #endregion
}