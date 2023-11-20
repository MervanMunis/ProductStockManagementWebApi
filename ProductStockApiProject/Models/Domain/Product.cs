using System;
using System.ComponentModel.DataAnnotations;

namespace ProductStockApiProject.Models.Domain
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime DeletedAt { get; set; } = DateTime.MinValue;
        public bool IsDeleted { get; set; } = false;

        // Navigation Properties
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
    }
}
