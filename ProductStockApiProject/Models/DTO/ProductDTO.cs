using System.ComponentModel.DataAnnotations;
using ProductStockApiProject.Models.DTO;

namespace ProductStockApiProject.Models.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<StockDTO>? Stocks { get; set; } // Include stocks in the DTO
    }
}
