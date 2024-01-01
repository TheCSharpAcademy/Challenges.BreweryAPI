﻿using System.ComponentModel.DataAnnotations;

namespace BreweryApi.Models
{
    public class Beer
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Flavour { get; set; }
        public string Age { get; set; }
        public int BreweryId { get; set; }
        public decimal BreweryPrice {  get; set; }
    }
}
