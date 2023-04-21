using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.Product;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace managing_humanitarian_collections_api.Models.Validators
{
    public class CreateProductCategoryDtoValidator : AbstractValidator<CreateProductCategoryDto>
    {
        private readonly ManagingCollectionsDbContext _dbContext;

        public CreateProductCategoryDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50)
                .MustAsync((name, cancellation) => NameIsUnique(name))
                .WithMessage("Category with this name already exists.");
        }

        private async Task<bool> NameIsUnique(string name)
        {
            return await _dbContext.ProductCategories
                .AllAsync(category => category.Name != name);
        }
    }
}