using Desk.Identity.Handlers.Token.Commands;
using Desk.Identity.Options;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Desk.Identity.Handlers.Token;

public class GenerateHandler : IRequestHandler<GenerateCommand, string>
{
    private readonly JwtOptions _jwtOptions;

    public GenerateHandler(
        IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }

    public async Task<string> Handle(GenerateCommand command, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(_jwtOptions.ExparedInMinutes);

        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: new List<Claim> { new Claim("id", command.Id.ToString()), new Claim("userName", command.UserName) },
            notBefore: now,
            expires: expires,
            signingCredentials: new SigningCredentials(_jwtOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
        );

        return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
}