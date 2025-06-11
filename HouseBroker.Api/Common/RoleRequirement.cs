using Microsoft.AspNetCore.Authorization;

namespace HouseBroker.Api.Common
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string[] AllowedRoles { get; }

        public RoleRequirement(params string[] allowedRoles)
        {
            AllowedRoles = allowedRoles;
        }
    }
}
