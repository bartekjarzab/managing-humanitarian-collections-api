using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.UserModels;
using System.Linq;

namespace managing_humanitarian_collections_api.Models.Validadors
{
    public class AddProductToCategoryDtoValidator : AbstractValidator<AddProductToCategoryDto>
    {
        public AddProductToCategoryDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(40);


            RuleFor(x => x.Name)
                .Custom((value, context) =>
                {
                    var productInCategory = dbContext.Products.Any(u => u.Name == value);
                    if (productInCategory)
                    {
                        context.AddFailure("Nazwa produktu", "produkt już istnieje w tej kategorii");
                    }
                });
        }
    }
}
