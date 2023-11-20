namespace ProductStockApiProject.Models.DTO
{
    public class UpdateProductRequestDto
    {
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
       
    }
}
