using HouseBroker.Infra.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace HouseBroker.Api.Common
{
    public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtTokenService _jwtTokenService;

        public RoleRequirementHandler(IHttpContextAccessor httpContextAccessor, IJwtTokenService jwtTokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtTokenService = jwtTokenService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (!httpContext.Request.Headers.TryGetValue("Authorization", out var tokenStr))
            {
                return Task.CompletedTask;
            }

            var token = tokenStr.ToString().Replace("Bearer ", "");
            var tdata = _jwtTokenService.GetTokenData(token);

            if (tdata == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.AllowedRoles.Contains(tdata.RoleDescription))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
