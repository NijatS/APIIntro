﻿using Microsoft.AspNetCore.Http;

namespace APIIntro.Service.Dtos.Categories
{
    public record ProductPostDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public IFormFile file { get; set; }
        public int CategoryId { get; set; }
    }
}
