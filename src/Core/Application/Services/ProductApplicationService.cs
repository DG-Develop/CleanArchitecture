using ECommerce.Application.Interfaces;
using ECommerce.Application.Features.Products.DTOS;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.ValueObject.Products;

namespace ECommerce.Application.Services
{
    /// <summary>
    /// Example implementation of IProductApplicationService showing how
    /// Application layer coordinates Domain interfaces and Application interfaces.
    /// 
    /// This demonstrates the difference between:
    /// - Domain interfaces (IProductRepository) - owned by domain
    /// - Application interfaces (INotificationService, ICacheService) - owned by application
    /// </summary>
    public class ProductApplicationService : IProductApplicationService
    {
        // Domain interfaces - these are REQUIRED by domain logic
        private readonly IProductRepository _productRepository;
        private readonly ICatProductsType _categoryRepository;

        // Application interfaces - these coordinate workflows and external concerns
        private readonly INotificationService _notificationService;
        private readonly ICacheService _cacheService;
        private readonly IFileStorageService _fileStorageService;

        public ProductApplicationService(
            IProductRepository productRepository,
            ICatProductsType categoryRepository,
            INotificationService notificationService,
            ICacheService cacheService,
            IFileStorageService fileStorageService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _notificationService = notificationService;
            _cacheService = cacheService;
            _fileStorageService = fileStorageService;
        }

        public async Task<ProductResultDTO> CreateProductWithNotificationAsync(CreateProductRequest request)
        {
            // 1. Domain operation using domain interface (IProductRepository)
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                IdCoin = request.IdCoin,
                IdCatProductType = request.IdCatProductType,
                Active = true
            };

            var createdProduct = await _productRepository.AddAsync(product);
            
            if (createdProduct == null)
                throw new InvalidOperationException("Failed to create product");

            // 2. Application-level coordination using application interfaces
            
            // Cache the new product
            var cacheKey = $"product:{createdProduct.Id}";
            await _cacheService.SetAsync(cacheKey, createdProduct, TimeSpan.FromHours(1));

            // Send notification (external concern handled by application)
            await _notificationService.SendProductCreatedNotificationAsync(
                createdProduct.Id, 
                "admin@ecommerce.com");

            // 3. Return application-specific DTO
            return new ProductResultDTO
            {
                Name = createdProduct.Name,
                Description = createdProduct.Description,
                PriceCoin = "USD" // This would come from coin lookup
            };
        }

        public async Task<ProductResultDTO> UpdateProductWithCacheInvalidationAsync(UpdateProductRequest request)
        {
            // 1. Get existing product using domain interface
            var existingProduct = await _productRepository.GetByIdAsync(request.Id);
            if (existingProduct == null)
                throw new ArgumentException("Product not found");

            var oldPrice = existingProduct.Price;

            // 2. Update using domain interface
            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;
            existingProduct.IdCoin = request.IdCoin;
            existingProduct.IdCatProductType = request.IdCatProductType;

            await _productRepository.UpdateAsync(existingProduct);

            // 3. Application-level coordination
            
            // Invalidate cache
            await _cacheService.RemoveProductCacheAsync(request.Id);

            // Notify about significant price changes
            if (Math.Abs(oldPrice - request.Price) > oldPrice * 0.1m) // 10% change
            {
                await _notificationService.SendPriceChangeNotificationAsync(
                    request.Id, oldPrice, request.Price, "admin@ecommerce.com");
            }

            return new ProductResultDTO
            {
                Name = existingProduct.Name,
                Description = existingProduct.Description,
                PriceCoin = "USD"
            };
        }

        public async Task<bool> SoftDeleteProductAsync(int productId, string deletedByUser)
        {
            // Domain operation
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null) return false;

            product.Active = false;
            await _productRepository.UpdateAsync(product);

            // Application coordination
            await _cacheService.RemoveProductCacheAsync(productId);
            
            // Clean up associated files
            await _fileStorageService.DeleteProductImageAsync($"product_{productId}.jpg", productId);

            return true;
        }

        public async Task<ProductDetailValueJoin> GetProductWithEnrichedDataAsync(int productId)
        {
            // Try cache first (application concern)
            var cacheKey = $"product_detail:{productId}";
            var cached = await _cacheService.GetAsync<ProductDetailValueJoin>(cacheKey);
            if (cached != null) return cached;

            // Get from domain (domain interface)
            var productResult = await _productRepository.GetProductJoinCoinById(productId);
            if (productResult == null)
                throw new ArgumentException("Product not found");

            // Get image URL (application concern)
            var imageUrl = await _fileStorageService.GetProductImageUrlAsync(productId);

            var enrichedProduct = new ProductDetailValueJoin
            {
                Name = productResult.Name,
                Description = productResult.Description,
                Price = 0, // Would need to get from product entity
                PriceCoin = productResult.PriceCoin,
                Descrip = imageUrl ?? "no-image.jpg"
            };

            // Cache result
            await _cacheService.SetAsync(cacheKey, enrichedProduct, TimeSpan.FromMinutes(30));

            return enrichedProduct;
        }

        public async Task<BulkImportResult> ImportProductsBulkAsync(IEnumerable<ProductImportDto> products)
        {
            var result = new BulkImportResult();
            result.TotalProcessed = products.Count();

            foreach (var productDto in products)
            {
                try
                {
                    // This would involve multiple domain operations
                    // and application coordination - a complex workflow
                    
                    var product = new Product
                    {
                        Name = productDto.Name,
                        Description = productDto.Description,
                        Price = productDto.Price,
                        // Would need to lookup coin and category by name
                        IdCoin = 1, // Simplified
                        IdCatProductType = 1, // Simplified
                        Active = true
                    };

                    await _productRepository.AddAsync(product);
                    result.SuccessfulImports++;
                }
                catch (Exception ex)
                {
                    result.FailedImports++;
                    result.ErrorMessages.Add($"Failed to import {productDto.Name}: {ex.Message}");
                }
            }

            return result;
        }
    }
}