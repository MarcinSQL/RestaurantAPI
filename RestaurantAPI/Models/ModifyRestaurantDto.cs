﻿using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class ModifyRestaurantDto
    {
        [MaxLength(25)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? HasDelivery { get; set; }
    }
}
