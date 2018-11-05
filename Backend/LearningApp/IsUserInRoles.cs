using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LearningApp.API
{
    public class UserInRoles : IAuthorizationRequirement
    {
        public string role;

        public UserInRoles(string role)
        {
            this.role = role;
        }
    }
    public class UserInRolesHandler
       : AuthorizationHandler<UserInRoles>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, UserInRoles requirement)
        {

            var role = context.User
               .FindFirst(claim => claim.Type == ClaimTypes.Role).Value;

            if (role == requirement.role)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

    }

    public static class CustomClaimTypes
    {
        public const string Roles = "Roles";
    }
}
