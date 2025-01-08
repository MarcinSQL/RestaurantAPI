using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Services;

namespace RestaurantAPI.Authorization
{
    public class CreatedMultipleRestaurantsRequirementHandler : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        private readonly IUserContextService _userContextService;
        private readonly RestaurantDbContext _dbContext;

        public CreatedMultipleRestaurantsRequirementHandler(IUserContextService userContextService, RestaurantDbContext dbContext)
        {
            _userContextService = userContextService;
            _dbContext = dbContext;
            
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
        {
            var userId = _userContextService.GetUserId;

            var restaurantCount = _dbContext
                .Restaurants
                .Count(r => r.CreatedById == userId);

            if(restaurantCount >= requirement.MinimumRestaurants)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
