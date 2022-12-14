using managing_humanitarian_collections_api.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace managing_humanitarian_collections_api.Authorization
{
    public class ResourceOperationRequirementHandler
       : AuthorizationHandler<ResourceOperationRequirement, Collection>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Collection Collection)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (Collection.OrganizerId == int.Parse(userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
