using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.Order;
using managing_humanitarian_collections_api.Models.UserModels;
using System.Linq;

namespace managing_humanitarian_collections_api.Models.Validators
{
    public class UpdateOrderStatusDtoValidator : AbstractValidator<UpdateOrderStatusDto>
    {
        public UpdateOrderStatusDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            RuleFor(x => x.OrderStatusId)
                .NotEmpty()
                .ExclusiveBetween(0, 4);
        }
    }
}
