using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.Collection;
using System.Linq;
using System.Text.RegularExpressions;

namespace managing_humanitarian_collections_api.Models.Validators
{
    public class EditCollectionDtoValidator : AbstractValidator<EditCollectionDto>
    {
        public EditCollectionDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            RuleFor(x => x.RegistrationNumber)
            .NotEmpty()
            .Matches(new Regex(@"^\d{4}\/\d{0,5}\/[a-z]{2}$", RegexOptions.IgnoreCase))
            .WithMessage("Numer rejestracyjny jest nieprawidłowy. Powinien być w formacie YYYY/NNNNN/LL, gdzie Y - rok, N - numer, L - litery.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(100);
            
            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(50)
                .MaximumLength(1500);
            RuleFor(x => x.CollectionStatusId)
                .NotEmpty()
                .ExclusiveBetween(0, 3);

            RuleFor(x => x.RegistrationNumber)
                .Custom((value, context) =>
                {
                    var registrationNumber = dbContext.Collections.Any(u => u.RegistrationNumber == value);
                    if (registrationNumber)
                    {
                        context.AddFailure("Error", "Zbiórka o tym numerze rejestracyjnym już istnieje");
                    }
                });
        }
    }
}




