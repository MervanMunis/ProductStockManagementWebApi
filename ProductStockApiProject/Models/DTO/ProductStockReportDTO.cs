namespace ProductStockApiProject.Models.DTO
{
    public class ProductStockReportDTO
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public Guid StockId { get; set; }
        public int StockQuantity { get; set; }
        public DateTime StockCreatedAt { get; set; }
    }
}