using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.Comment;

namespace managing_humanitarian_collections_api.Models.Validators
{
    public class CreaterCommentDtoValidator : AbstractValidator<CreateCommentDto>
    {
        public CreaterCommentDtoValidator(ManagingCollectionsDbContext dbContext)
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(500);
        }
    }
}
