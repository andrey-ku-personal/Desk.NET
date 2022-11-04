using AutoMapper;
using Desk.Identity.Handlers.Token.Commands;
using Desk.Identity.Handlers.User.Commands;
using Desk.Identity.Handlers.User.Models;
using Desk.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Desk.Identity.Handlers.Token;

public class AuthorizeHandler : IRequestHandler<AuthorizeCommand, string>
{
    private readonly IUserPasswordStore<UserModel> _passwordStore;
    private readonly IPasswordHasher<UserModel> _passwordhasher;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthorizeHandler(IUserPasswordStore<UserModel> passwordStore
        , IPasswordHasher<UserModel> passwordhasher
        , IMediator mediator
        , IMapper mapper)
    {
        _passwordStore = passwordStore;
        _passwordhasher = passwordhasher;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<string> Handle(AuthorizeCommand command, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(_mapper.Map<GetCommand>(command), cancellationToken);

        if (user == null)
        {
            throw new NotFoundException($"User/'{command.UserNameOrEmail}' was not found");
        }

        var passwordHash = await _passwordStore.GetPasswordHashAsync(user, cancellationToken);

        if (_passwordhasher.VerifyHashedPassword(user, passwordHash, command.Password) == PasswordVerificationResult.Failed)
        {
            throw new NotFoundException($"Wrong password");
        }

        return await _mediator.Send(_mapper.Map<GenerateCommand>(user), cancellationToken);
    }
}