using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.Collection;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace managing_humanitarian_collections_api.Models.Validators
{
    public class CreateCollectionProductDtoValidator : AbstractValidator<CreateCollectionProductDto>
    {
        private readonly ManagingCollectionsDbContext _dbContext;

        public CreateCollectionProductDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Quantily)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
            RuleFor(x => x.ShortDescription)
                .MaximumLength(100);

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .MustAsync((productId, cancellationToken) => ProductExists(productId, _dbContext))
                .WithMessage("Przedmiot z takim Id nie istnieje");
        }

        private async Task<bool> ProductExists(int productId, ManagingCollectionsDbContext dbContext)
        {
            return await dbContext.Products.AnyAsync(p => p.Id == productId);
        }
    }
}