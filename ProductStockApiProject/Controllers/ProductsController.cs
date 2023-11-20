using Microsoft.AspNetCore.Mvc;
using ProductStockApiProject.Models.DTO;
using ProductStockApiProject.Services;

namespace ProductStockApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService productService;

        public ProductsController(ProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var productsDto = productService.GetAllProducts();
            return Ok(productsDto);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetProductById(Guid id)
        {
            var productDto = productService.GetProductById(id);

            if (productDto == null)
            {
                return NotFound();
            }

            return Ok(productDto);
        }

        // Post to create new product
        [HttpPost]
        public IActionResult CreateProduct([FromBody] AddProductRequestDto addProductRequestDto)
        {
            var createdProductDto = productService.CreateProduct(addProductRequestDto);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProductDto.Id }, createdProductDto);
        }

        // Put to Update Product 
        [HttpPut("{id:Guid}")]
        public IActionResult UpdateProduct(Guid id, [FromBody] UpdateProductRequestDto updateProductRequestDto)
        {
            var updatedProductDto = productService.UpdateProduct(id, updateProductRequestDto);

            if (updatedProductDto == null)
            {
                return NotFound();
            }

            return Ok(updatedProductDto);
        }

        // Delete to Delete Prouct
        [HttpDelete("{id:Guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var isDeleted = productService.DeleteProduct(id);

            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }


        // Integrate stock-related functionalities into the ProductsController.

        [HttpGet("{productId:Guid}/stocks")]
        public IActionResult GetProductStocks(Guid productId)
        {
            var stocksDto = productService.GetProductStocks(productId);

            if (stocksDto == null)
            {
                return NotFound();
            }

            return Ok(stocksDto);
        }

        [HttpPost("{productId:Guid}/stocks")]
        public IActionResult AddStockToProduct(Guid productId, [FromBody] int quantity)
        {
            var addedStockDto = productService.AddStockToProduct(productId, quantity);

            if (addedStockDto == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetProductStocks), new { productId = productId }, addedStockDto);
        }

        [HttpDelete("stocks/{stockId:Guid}")]
        public IActionResult RemoveStock(Guid stockId)
        {
            var isRemoved = productService.RemoveStock(stockId);

            if (!isRemoved)
            {
                return NotFound();
            }

            return NoContent();
        }

        // Report
        [HttpGet("stock-report")]
        public IActionResult GenerateProductStockReport()
        {
            var reportDto = productService.GenerateProductStockReport();
            return Ok(reportDto);
        }
    } 
}
