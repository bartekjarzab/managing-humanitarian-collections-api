using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.UserModels;
using System.Linq;

namespace managing_humanitarian_collections_api.Models.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .MaximumLength(40);


            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (!emailInUse)
                    {
                        context.AddFailure("Error", "Taki użytkownik nie istnieje");
                    }
                });
         
            }
        }
    }

