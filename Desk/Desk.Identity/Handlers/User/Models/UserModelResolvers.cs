using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Desk.Identity.Handlers.User.Models
{
    public class UserPasswordHashResolver<TSource> : IValueResolver<TSource, Domain.Entities.User, string>
        where TSource : UserModel
    {
        private readonly IPasswordHasher<UserModel> _passwordHasher;

        public UserPasswordHashResolver(IPasswordHasher<UserModel> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string Resolve(TSource source, Domain.Entities.User destination, string destMember, ResolutionContext context)
        {
            return _passwordHasher.HashPassword(source, source.Password);
        }
    }
}
