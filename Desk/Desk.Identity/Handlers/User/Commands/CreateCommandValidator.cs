using FluentValidation;
using System.Text.RegularExpressions;

namespace Desk.Identity.Handlers.User.Commands;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.Id).Equal(0);
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(126);
        RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(126);
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(x => x.Password).Must(ValidPassword).MaximumLength(256);

        When(r => !string.IsNullOrWhiteSpace(r.Description), () =>
        {
            RuleFor(r => r.Description).MaximumLength(2048);
        });

        When(r => !string.IsNullOrWhiteSpace(r.Website), () =>
        {
            RuleFor(r => r.Website).MaximumLength(1024);
        });
    }

    private bool ValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (password.Length < 12)
            return false;

        if (!password.Any(x => char.IsUpper(x)))
            return false;

        if (!password.Any(x => char.IsLower(x)))
            return false;

        if (!password.Any(x => char.IsLetter(x)))
            return false;

        if (!password.Any(x => char.IsNumber(x)))
            return false;

        if (new Regex("^[a-zA-Z0-9 ]*$").IsMatch(password))
            return false;

        return true;
    }
}