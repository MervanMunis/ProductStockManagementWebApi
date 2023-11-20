using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductStockApiProject.Models.Domain
{
    public class Stock
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }

        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; } // Foreign key
        public virtual Product? Product { get; set; } // Navigation property

    }
}
