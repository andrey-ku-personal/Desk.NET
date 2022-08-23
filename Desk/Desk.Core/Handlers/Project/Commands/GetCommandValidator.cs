using FluentValidation;

namespace Desk.Core.Handlers.Project.Commands;

public class GetCommandValidator : AbstractValidator<GetCommand>
{
    public GetCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}