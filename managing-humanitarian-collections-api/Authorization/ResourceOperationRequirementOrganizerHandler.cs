using managing_humanitarian_collections_api.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace managing_humanitarian_collections_api.Authorization
{
    public class ResourceOperationRequirementOrganizerHandler
       : AuthorizationHandler<ResourceOperationRequirement, Collection>
    {
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement,
                Collection collection)
            {
                if (requirement.ResourceOperation == ResourceOperation.Read ||
                    requirement.ResourceOperation == ResourceOperation.Create)
                {
                    context.Succeed(requirement);
                }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
            if (collection.CreatedByOrganiserId == int.Parse(userId) || (userRole == "Admin" ))
                {
                    context.Succeed(requirement);
                }

                return Task.CompletedTask;
            }
        }
    }
