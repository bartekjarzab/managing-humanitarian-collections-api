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
                .MinimumLength(3)
                .MaximumLength(5);
            RuleFor(x => x.ClosingHour)
                .NotEmpty()
                .Length(5);
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(5);
            RuleFor(x => x.Postcode)
                .NotEmpty()
                .Length(6);
            RuleFor(x => x.City)
                .NotEmpty();
                


            RuleFor(x => x.Name)
                .Custom((value, context) =>
                {
                    var productInCategory = dbContext.Products.Any(u => u.Name == value);
                    if (productInCategory)
                    {
                        context.AddFailure("Produkt", "produkt już istnieje w tej kategorii");
                    }
                });
        }
    }
}
