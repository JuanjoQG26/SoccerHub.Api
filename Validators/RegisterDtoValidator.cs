using FluentValidation;
using SoccerHub.Api.DTOs;

namespace SoccerHub.Api.Validators
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDTO>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MinimumLength(3)
                .WithMessage("Minimum 3 characters")
                .MaximumLength(100)
                .WithMessage("Maximum 100 characteres");

            RuleFor(x => x.Email)
                .NotEmpty()
               .EmailAddress()
               .WithMessage("Invalid email");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password must have at least 6 characters");
        }
    }
}
