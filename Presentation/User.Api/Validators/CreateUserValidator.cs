using User.Domain.Contract.Requests;
using FluentValidation;

namespace User.Api.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(255)
            .MinimumLength(1);
    }
}