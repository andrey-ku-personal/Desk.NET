using FluentValidation;

namespace Desk.Core.Handlers.Project.Commands;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.Id).Equal(0);
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(256);
        When(r => !string.IsNullOrWhiteSpace(r.Description), () => 
        {
            RuleFor(r => r.Description).MaximumLength(1024);
        });
        RuleFor(x => x.Description).NotNull().NotEmpty();
    }
}