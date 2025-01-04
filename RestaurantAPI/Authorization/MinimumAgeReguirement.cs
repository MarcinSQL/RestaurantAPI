using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class MinimumAgeReguirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; }
        public MinimumAgeReguirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
