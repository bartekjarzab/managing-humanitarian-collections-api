
using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.Collection;


namespace managing_humanitarian_collections_api.Models.Validators
{
    public class UpdateCollectionStatusDtoValidator : AbstractValidator<UpdateCollectionStatusDto>
    {
        public UpdateCollectionStatusDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            RuleFor(x => x.CollectionStatusId)
                .NotEmpty()
                .ExclusiveBetween(0, 3);
        }
    }
}
