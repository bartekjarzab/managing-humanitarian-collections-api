using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.UserModels;
using System.Linq;

namespace managing_humanitarian_collections_api.Models.Validadors
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUser = dbContext.Users.Any(u => u.Email == value);
                    if(emailInUser)
                    {
                        context.AddFailure("Email", "Adres mailowy jest zajety");
                    }    
                });
        }
    }
}
