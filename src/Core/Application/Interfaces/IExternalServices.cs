namespace ECommerce.Application.Interfaces
{
    /// <summary>
    /// External service interface for sending notifications.
    /// This belongs in Application layer because:
    /// 1. It's not needed by the domain directly
    /// 2. It's used by application services to coordinate workflows
    /// 3. It represents an external concern that application layer coordinates
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Sends email notification when a product is created
        /// </summary>
        Task SendProductCreatedNotificationAsync(int productId, string recipientEmail);

        /// <summary>
        /// Sends notification when product stock is low
        /// </summary>
        Task SendLowStockAlertAsync(int productId, int currentStock, string recipientEmail);

        /// <summary>
        /// Sends notification when product price changes significantly
        /// </summary>
        Task SendPriceChangeNotificationAsync(int productId, decimal oldPrice, decimal newPrice, string recipientEmail);
    }

    /// <summary>
    /// Service for handling file operations like image uploads.
    /// Application layer interface because it coordinates external file operations
    /// that are not core domain concerns but are needed for use cases.
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        /// Uploads product image and returns the file URL
        /// </summary>
        Task<string> UploadProductImageAsync(Stream imageStream, string fileName, int productId);

        /// <summary>
        /// Deletes product image from storage
        /// </summary>
        Task<bool> DeleteProductImageAsync(string fileName, int productId);

        /// <summary>
        /// Gets product image URL by product ID
        /// </summary>
        Task<string?> GetProductImageUrlAsync(int productId);
    }

    /// <summary>
    /// Caching service interface for application-level caching strategies.
    /// Application layer because it represents caching concerns that are
    /// orchestrated by application services, not domain logic.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Gets cached product data
        /// </summary>
        Task<T?> GetAsync<T>(string key) where T : class;

        /// <summary>
        /// Sets product data in cache
        /// </summary>
        Task SetAsync<T>(string key, T value, TimeSpan expiration) where T : class;

        /// <summary>
        /// Removes product data from cache
        /// </summary>
        Task RemoveAsync(string key);

        /// <summary>
        /// Removes all cached data for a product
        /// </summary>
        Task RemoveProductCacheAsync(int productId);
    }
}