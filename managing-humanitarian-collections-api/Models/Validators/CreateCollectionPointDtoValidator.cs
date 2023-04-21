using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.Collection;
using managing_humanitarian_collections_api.Models.Products;
using managing_humanitarian_collections_api.Models.UserModels;
using System.Linq;

namespace managing_humanitarian_collections_api.Models.Validators
{
    public class CreateCollectionPointDtoValidator : AbstractValidator<CreateCollectionPointDto>
    {
        public CreateCollectionPointDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            RuleFor(x => x.OpeningHour)
                  .NotEmpty()
                  .Matches(@"^([0-1][0-9]|[2][0-3]):[0-5][0-9]$");
            RuleFor(x => x.ClosingHour)
                .NotEmpty()
                .Matches(@"^([0-1][0-9]|[2][0-3]):[0-5][0-9]$");
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(60);
            RuleFor(x => x.Postcode)
                 .NotEmpty()
                 .Matches(@"^[0-9]{2}-[0-9]{3}$");
            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Street)
                .MaximumLength(50);
            RuleFor(x => x.HouseNumber)
                .MaximumLength(8);
            RuleFor(x => x.Apartment)
                .MaximumLength(6);
            RuleFor(x => x.VoivodeshipId)
                .NotEmpty()
                .ExclusiveBetween(0, 3);





        }
    }
}
