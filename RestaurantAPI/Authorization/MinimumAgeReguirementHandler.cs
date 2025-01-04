using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RestaurantAPI.Authorization
{
    public class MinimumAgeReguirementHandler : AuthorizationHandler<MinimumAgeReguirement>
    {
        private readonly ILogger<MinimumAgeReguirementHandler> _logger;
        public MinimumAgeReguirementHandler(ILogger<MinimumAgeReguirementHandler> logger)
        {
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeReguirement requirement)
        {
            var dateOfBirthClaim = context.User.FindFirst(c => c.Type == "DateOfBirth");
            var userEmailClaim = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);

            if (dateOfBirthClaim == null || userEmailClaim == null)
            {
                _logger.LogInformation("Required claims are missing");
                context.Fail();
                return Task.CompletedTask;
            }

            var dateOfBirth = DateTime.Parse(dateOfBirthClaim.Value);
            var userEmail = userEmailClaim.Value;

            _logger.LogInformation($"User {userEmail} with date of birth: [{dateOfBirth}]");

            if (dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            {
                _logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Authorization failed");
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
