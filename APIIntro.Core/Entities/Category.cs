using APIIntro.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace APIIntro.Core.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<Product>? Products { get; set; } 
    }
}
