using FluentValidation;
using managing_humanitarian_collections_api.Entities;

using managing_humanitarian_collections_api.Models.User;

namespace managing_humanitarian_collections_api.Models.Validators
{
    public class EditProfileDtoValidator : AbstractValidator<EditProfileDto>
    {
        public EditProfileDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            RuleFor(x => x.FirstName)
                  .Matches("^[a-zA-Z]+$");
            RuleFor(x => x.LastName)
              .Matches("^[a-zA-Z]+$");
            RuleFor(x => x.Name)
                .Matches("^[a-zA-Z]+$")
                .MaximumLength(50);
            RuleFor(x => x.Nip)
                 .InclusiveBetween(1000000000, 9999999999);
            RuleFor(x => x.Regon)
               .InclusiveBetween(100000000, 999999999);
            RuleFor(x => x.AvatarId)
                 .ExclusiveBetween(0, 3);
            RuleFor(x => x.ContactNumber)
                  .Matches("^\\d{9}$|^\\d{3}-\\d{3}-\\d{3}$");


        }
    }
}


