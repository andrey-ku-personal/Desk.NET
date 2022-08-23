using FluentValidation;

namespace Desk.Core.Handlers.Project.Commands;

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}