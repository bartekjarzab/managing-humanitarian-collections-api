
using managing_humanitarian_collections_api.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace managing_humanitarian_collections_api.Authorization
{
    public class ResourceOperationRequirementDonatorHandler
        : AuthorizationHandler<ResourceOperationRequirement, Order>
    {
        private readonly ManagingCollectionsDbContext _dbContext;

        public ResourceOperationRequirementDonatorHandler(ManagingCollectionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Order order)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read ||
                requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;

            if (order.CreatedById == int.Parse(userId) || userRole == "Admin" || userRole == "Organizator")
            {
                context.Succeed(requirement);
            }
            else
            {
                var collection = await _dbContext.Collections.FirstOrDefaultAsync(c => c.Id == order.CollectionId);

                if (collection.CreatedByOrganiserId == int.Parse(userId))
                {
                    context.Succeed(requirement);
                }
            }

            return;
        }
    }
}