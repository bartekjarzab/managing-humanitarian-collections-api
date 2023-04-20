using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.Collection;
using managing_humanitarian_collections_api.Models.UserModels;
using System.Linq;

namespace managing_humanitarian_collections_api.Models.Validators
{
    public class CreateCollectionDtoValidator : AbstractValidator<CreateCollectionDto>
    {
        public CreateCollectionDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            RuleFor(x => x.RegistrationNumber)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(15);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(50);
            
            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(50)
                .MaximumLength(1500);
            //RuleFor(x => x.CollectionStatusId)
            //    .NotEmpty()
            //    .ExclusiveBetween(0,3);

            RuleFor(x => x.RegistrationNumber)
                .Custom((value, context) =>
                {
                    var registrationNumber = dbContext.Collections.Any(u => u.RegistrationNumber == value);
                    if (registrationNumber)
                    {
                        context.AddFailure("Numer rejestracyjny", "Zbiórka o tym numerze rejestracyjnym już istnieje");
                    }
                });
        }
    }
}




//public string Title { get; set; }

//public string Description { get; set; }

//public int CollectionStatusId { get; set; }