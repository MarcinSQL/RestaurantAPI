using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IRestaurantSerivce
    {
        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetById(int id);
        void Delete(int id);
        void Modify(int id, ModifyRestaurantDto dto);
    }

    public class RestaurantSerivce : IRestaurantSerivce
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantSerivce> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _UserContextService;
        public RestaurantSerivce(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantSerivce> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _UserContextService = userContextService;
        }

        public void Modify(int id, ModifyRestaurantDto dto)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found.");

            var authorizationResulte = _authorizationService.AuthorizeAsync(_UserContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Modify)).Result;

            if (!authorizationResulte.Succeeded)
            {
                throw new ForbidException();
            }

            if (dto.Name != null)
            {
                restaurant.Name = dto.Name;
            }
            if(dto.Description != null)
            {
                restaurant.Description = dto.Description;
            }
            if(dto.HasDelivery != null)
            {
                restaurant.HasDelivery = (bool)dto.HasDelivery;
            }

            _dbContext.Restaurants.Update(restaurant);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _logger.LogError($"Restaurant with id: {id} DELETE action invoked.");

            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found.");

            var authorizationResulte = _authorizationService.AuthorizeAsync(_UserContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResulte.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found.");
            }

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            //One way of mapping
            //var restaurantsDtos = restaurants.Select(r => new RestaurantDto()
            //{
            //    Name = r.Name,
            //    Category = r.Category,
            //    City = r.Address.City,
            //    ...
            //})

            //Another (easier) way of mapping
            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantsDtos;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = _UserContextService.GetUserId;
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }
    }
}
