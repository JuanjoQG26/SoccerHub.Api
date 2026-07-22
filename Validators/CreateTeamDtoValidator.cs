using FluentValidation;
using SoccerHub.Api.DTOs;

namespace SoccerHub.Api.Validators
{
    public class CreateTeamDtoValidator:AbstractValidator<CrearTeamDto>
    {
        public CreateTeamDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Team name is required")
                .MinimumLength(3)
                .WithMessage("Minimum 3 characters")
                .MaximumLength(100)
                .WithMessage("Maximum 100 characters");
        }
    }
}
