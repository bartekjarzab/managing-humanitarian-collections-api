using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.Products;
using managing_humanitarian_collections_api.Models.UserModels;
using System.Linq;

namespace managing_humanitarian_collections_api.Models.Validators
{
    public class AddProductToCategoryDtoValidator : AbstractValidator<AddProductToCategoryDto>
    {
        public AddProductToCategoryDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(40);

            RuleFor(x => x.Size)
                .MaximumLength(40);


            RuleFor(x => x)
                .Custom((dto, context) =>
                {
                    var productInCategory = dbContext.Products.Any(u => u.Name == dto.Name &&  u.Size == dto.Size);
                    if (productInCategory)
                    {
                        context.AddFailure("Error", "Przedmiot już istnieje w tej kategorii");
                    }
                });
        }
    }
}
