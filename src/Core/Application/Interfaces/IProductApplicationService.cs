using ECommerce.Application.Features.Products.DTOS;
using ECommerce.Domain.ValueObject.Products;

namespace ECommerce.Application.Interfaces
{
    /// <summary>
    /// Application service interface for coordinating product-related use cases.
    /// This interface belongs in the Application layer because it orchestrates
    /// multiple domain operations and handles application-specific workflows.
    /// </summary>
    public interface IProductApplicationService
    {
        /// <summary>
        /// Creates a new product with business rule validation and notifications.
        /// This coordinates multiple domain operations beyond just persistence.
        /// </summary>
        Task<ProductResultDTO> CreateProductWithNotificationAsync(CreateProductRequest request);

        /// <summary>
        /// Updates product information and handles side effects like cache invalidation.
        /// Application-level coordination that involves multiple concerns.
        /// </summary>
        Task<ProductResultDTO> UpdateProductWithCacheInvalidationAsync(UpdateProductRequest request);

        /// <summary>
        /// Soft deletes a product and handles cleanup of related data.
        /// Business workflow that requires coordination of multiple operations.
        /// </summary>
        Task<bool> SoftDeleteProductAsync(int productId, string deletedByUser);

        /// <summary>
        /// Gets product with enriched information for display purposes.
        /// Application-specific data aggregation and transformation.
        /// </summary>
        Task<ProductDetailValueJoin> GetProductWithEnrichedDataAsync(int productId);

        /// <summary>
        /// Bulk operation for importing products from external sources.
        /// Complex application workflow involving validation, transformation, and persistence.
        /// </summary>
        Task<BulkImportResult> ImportProductsBulkAsync(IEnumerable<ProductImportDto> products);
    }

    /// <summary>
    /// Request DTOs for the application service
    /// </summary>
    public class CreateProductRequest
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int IdCoin { get; set; }
        public int IdCatProductType { get; set; }
        public string CreatedByUser { get; set; } = null!;
    }

    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int IdCoin { get; set; }
        public int IdCatProductType { get; set; }
        public string UpdatedByUser { get; set; } = null!;
    }

    public class ProductImportDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string CoinCode { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
    }

    public class BulkImportResult
    {
        public int TotalProcessed { get; set; }
        public int SuccessfulImports { get; set; }
        public int FailedImports { get; set; }
        public List<string> ErrorMessages { get; set; } = new();
    }
}