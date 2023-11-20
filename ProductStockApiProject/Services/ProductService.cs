using Microsoft.EntityFrameworkCore;
using ProductStockApiProject.Data;
using ProductStockApiProject.Models.Domain;
using ProductStockApiProject.Models.DTO;

namespace ProductStockApiProject.Services
{
    public class ProductService
    {
        private readonly AppDbContext dbcontext;

        public ProductService(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        // CRUD operations will be implemented here
        public IEnumerable<ProductDTO> GetAllProducts()
        {
            var productsDomain = dbcontext.Products.ToList();
            return productsDomain.Select(p => MapProductDomainToDTO(p));
        }

        // 1. Read: Get Product By Using Its Id
        public ProductDTO? GetProductById(Guid id)
        {
            var productDomain = dbcontext.Products.FirstOrDefault(p => p.Id == id);
            return productDomain != null ? MapProductDomainToDTO(productDomain) : null;
        }

        // 2. Create: Create The Product 
        public ProductDTO CreateProduct(AddProductRequestDto addProductRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var productDomainModel = new Product
            {
                Name = addProductRequestDto.Name,
                CreatedAt = DateTime.Now,
                
            };

            // Map Domain model back to DTO
            var productDto = new ProductDTO()
            {
                Id = productDomainModel.Id,
                Name = productDomainModel.Name,
            };

            // Use Domain Model to Create Product
            dbcontext.Products.Add(productDomainModel);
            dbcontext.SaveChanges();

            return MapProductDomainToDTO(productDomainModel);
        }

        // 3. Update: Update The Product
        public ProductDTO? UpdateProduct(Guid id, UpdateProductRequestDto updateProductRequestDto)
        {
            var productDomainModel = dbcontext.Products.FirstOrDefault(p => p.Id == id);

            if (productDomainModel == null)
            {
                return null; // or throw NotFoundException
            }

            productDomainModel.Name = updateProductRequestDto.Name;
            dbcontext.SaveChanges();

            return MapProductDomainToDTO(productDomainModel);
        }

        // 4. Delete: Delete The Product
        public bool DeleteProduct(Guid id)
        {
            var productDomainModel = dbcontext.Products.FirstOrDefault(p => p.Id == id);

            if (productDomainModel == null)
            {
                return false; // or throw NotFoundException
            }

            dbcontext.Products.Remove(productDomainModel);
            dbcontext.SaveChanges();
            return true;
        }

        // Add methods for stock-related operations in the ProductService class
        public IEnumerable<StockDTO>? GetProductStocks(Guid ProductId)
        {
            var product = dbcontext.Products.Include(p => p.Stocks).FirstOrDefault(p => p.Id == ProductId);

            if (product == null)
            {
                return null; // or throw NotFoundException
            }

            return product.Stocks.Select(s => new StockDTO
            {
                Id = s.Id,
                Quantity = s.Quantity,
                CreatedAt = s.CreatedAt,
            });
        }

        // Add Stock to Product
        public StockDTO? AddStockToProduct(Guid productId, int quantity)
        {
            var product = dbcontext.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return null; // or throw NotFoundException
            }

            var stock = new Stock
            {
                Quantity = quantity,
                CreatedAt = DateTime.Now,
            };

            product.Stocks.Add(stock);
            dbcontext.SaveChanges();

            return new StockDTO
            {
                Id = stock.Id,
                Quantity = stock.Quantity,
                CreatedAt = stock.CreatedAt,
            };
        }

        // Remove the Stock
        public bool RemoveStock(Guid stockId)
        {
            var stock = dbcontext.Stocks.FirstOrDefault(s => s.Id == stockId);

            if (stock == null)
            {
                return false; // or throw NotFoundException
            }

            dbcontext.Stocks.Remove(stock);
            dbcontext.SaveChanges();

            return true;
        }

       // Generate Product Stock Report
        public IEnumerable<ProductStockReportDTO> GenerateProductStockReport()
        {
            var products = dbcontext.Products.Include(p => p.Stocks);

            var report = new List<ProductStockReportDTO>();

            foreach (var product in products)
            {
                foreach (var stock in product.Stocks)
                {
                    report.Add(new ProductStockReportDTO
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        StockId = stock.Id,
                        StockQuantity = stock.Quantity,
                        StockCreatedAt = stock.CreatedAt,
                    });
                }
            }

            return report;
        }

        // Mapping Product Domian Model To ProductDTO
        private static ProductDTO MapProductDomainToDTO(Product productDomain)
        {
            return new ProductDTO
            {
                Id = productDomain.Id,
                Name = productDomain.Name,
                CreatedAt = productDomain.CreatedAt,
                DeletedAt = productDomain.DeletedAt,
                IsDeleted = productDomain.IsDeleted,
            };
        }

        // Mapping Stock Domain Model To StockDTO
        private static StockDTO MapStockDomainToDTO(Stock stock)
        {
            return new StockDTO
            {
                Id = stock.Id,
                Quantity = stock.Quantity,
                CreatedAt = stock.CreatedAt,
            };
        }
    }
}

