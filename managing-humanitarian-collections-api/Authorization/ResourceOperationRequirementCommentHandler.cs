using managing_humanitarian_collections_api.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace managing_humanitarian_collections_api.Authorization
{
    public class ResourceOperationRequirementCommentHandler : AuthorizationHandler<ResourceOperationRequirement, Comment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Comment comment)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read || requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;

            if (comment.CreatedById == int.Parse(userId) || userRole == "Admin")
            {
                context.Succeed(requirement);
            }

            if (requirement.ResourceOperation == ResourceOperation.Update || requirement.ResourceOperation == ResourceOperation.Delete)
            {
                if (comment.CreatedById == int.Parse(userId))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}