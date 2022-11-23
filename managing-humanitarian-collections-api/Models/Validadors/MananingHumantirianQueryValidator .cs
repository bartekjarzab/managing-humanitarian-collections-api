using FluentValidation;
using managing_humanitarian_collections_api.Entities;
using managing_humanitarian_collections_api.Models.QueryValidators;
using System.Linq;

namespace managing_humanitarian_collections_api.Models.Validadors
{
    public class ManagingHumanitarianQueryValidator : AbstractValidator<ManagingHumanitarianQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };

       
        public ManagingHumanitarianQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });
        }
    }
}
