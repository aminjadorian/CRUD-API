using Domain.User;
using FluentValidation;

namespace Application.Users.Create;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IUserRepository userRepository)
    {

        RuleFor(c => c.Email)
            .MustAsync(async (email, _) =>
        {
            return !(await userRepository.isUserExistAsync(email));
        }).WithMessage("The email must be Unique")
        .NotEmpty().
        WithMessage("Please Enter Email")
        .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$").WithMessage("Please Enter a Valid Email");

        RuleFor(c => c.name.FirstName)
            .MaximumLength(50).WithMessage("Maximum length is 50 character")
            .MinimumLength(3).WithMessage("Minimum Length is 3 characters")
            .NotEmpty().WithMessage("Please Enter your First Name");

        RuleFor(c => c.name.LastName)
            .MaximumLength(50).WithMessage("Maximum length is 50 character")
            .MinimumLength(3).WithMessage("Minimum Length is 3 characters")
            .NotEmpty().WithMessage("Please Enter your First Name");
    }
}
