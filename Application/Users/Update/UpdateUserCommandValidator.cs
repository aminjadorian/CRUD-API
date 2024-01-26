using Domain.User;
using FluentValidation;

namespace Application.Users.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(IUserRepository userRepository)
    {
        RuleFor(r => r.Email).MustAsync(async (Email, _) =>
        {
            return !(await userRepository.isUserExistAsync(Email));
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
