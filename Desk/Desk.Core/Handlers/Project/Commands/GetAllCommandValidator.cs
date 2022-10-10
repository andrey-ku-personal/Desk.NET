using FluentValidation;

namespace Desk.Core.Handlers.Project.Commands;

public class GetAllCommandValidator : AbstractValidator<GetAllCommand>
{
    public GetAllCommandValidator()
    {
    }
}