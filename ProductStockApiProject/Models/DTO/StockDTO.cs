using System.ComponentModel.DataAnnotations;

namespace ProductStockApiProject.Models.DTO
{
    public class StockDTO
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
