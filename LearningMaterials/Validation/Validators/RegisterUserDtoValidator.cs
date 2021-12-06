using FluentValidation;
using LearningMaterials.Data;
using LearningMaterials.Models;
using System.Linq;

namespace LearningMaterials
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(MaterialsDbContext dbContext)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(6).WithMessage("Password should consist of at least 6 characters")
                .Matches("[a-z]").WithMessage("Password should consist of lower case letters");

            RuleFor(x => x)
                .Custom((value, context) =>
                {
                    if (value.Password != value.ConfirmPassword)
                    {
                        context.AddFailure(nameof(value.Password), "Passwords should match");
                    }
                });

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailUsed = dbContext.Users.Any(u => u.Email == value);
                    if (emailUsed)
                    {
                        context.AddFailure("Email", "This email address is already registered");
                    }
                });
        }
    }
}