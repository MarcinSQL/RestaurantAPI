using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Lousiville, Kentucky.",
                ContactEmail = "contact@kfc.com",
                ContactNumber = "111222333",
                HasDelivery = true,
                Dishes = new List<Dish>()
                {
                    new Dish()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Is a deep-fried chicken, slathered in a spicy hot paste and served on white bread with pickles on top.",
                        Price = 10.30M,
                    },

                    new Dish()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Breaded in a crispy coating, flavorfully spiced with a blend of aged cayenne and chili pepper, they're sure to get your attention.",
                        Price = 5.30M,
                    },
                },
                Address = new Address()
                {
                    City = "Kraków",
                    Street = "Długa 5",
                    PostalCode = "30-001"
                }
            },

                new Restaurant()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description = "Is an American multinational fast food chain, founded in 1940 as a restaurant operated by Richard and Maurice McDonald, in San Bernardino, California, United States.",
                ContactEmail = "contact@mcdonald.com",
                ContactNumber = "222333444",
                HasDelivery = true,
                Dishes = new List<Dish>()
                {
                    new Dish()
                    {
                        Name = "Big Mac",
                        Description = "Two chicken patties, a slice of cheese, lettuce, pickles. And the sauce. That unbeatable, tasty Big Mac sauce.",
                        Price = 4.50M,
                    },

                    new Dish()
                    {
                        Name = "McWrap",
                        Description = "A piece of crispy coated chicken with spicy mayo, lettuce and cheese, in a warm tortilla wrap. A delicious snack, every time.",
                        Price = 3.99M,
                    },
                },
                Address = new Address()
                {
                    City = "Kraków",
                    Street = "Krótka 12",
                    PostalCode = "30-001"
                }
            }
            };

            return restaurants;
        }

    }
}
