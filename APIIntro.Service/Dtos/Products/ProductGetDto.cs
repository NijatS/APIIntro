using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIIntro.Service.Dtos.Products
{
    public record ProductGetDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}
