using System.Data;

namespace ECommerce.Infrastructure.Interfaces
{
    /// <summary>
    /// Infrastructure-specific interface for database connection management.
    /// This belongs in Infrastructure because:
    /// 1. It's specific to database technology (SQL Server, MySQL, etc.)
    /// 2. It deals with low-level connection concerns
    /// 3. It's not needed by domain or application layers directly
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Creates a database connection for the current context
        /// </summary>
        IDbConnection CreateConnection();

        /// <summary>
        /// Creates a connection with specific connection string
        /// </summary>
        IDbConnection CreateConnection(string connectionString);

        /// <summary>
        /// Gets the default connection string
        /// </summary>
        string GetConnectionString();
    }

    /// <summary>
    /// Infrastructure interface for distributed caching implementations.
    /// Technology-specific interface that deals with Redis, MemoryCache, etc.
    /// </summary>
    public interface IDistributedCacheProvider
    {
        /// <summary>
        /// Gets value from distributed cache
        /// </summary>
        Task<byte[]?> GetAsync(string key);

        /// <summary>
        /// Sets value in distributed cache with expiration
        /// </summary>
        Task SetAsync(string key, byte[] value, TimeSpan expiration);

        /// <summary>
        /// Removes value from distributed cache
        /// </summary>
        Task RemoveAsync(string key);

        /// <summary>
        /// Checks if key exists in cache
        /// </summary>
        Task<bool> ExistsAsync(string key);
    }

    /// <summary>
    /// Infrastructure interface for message queue operations.
    /// Technology-specific for RabbitMQ, Azure Service Bus, etc.
    /// </summary>
    public interface IMessageQueueProvider
    {
        /// <summary>
        /// Publishes message to queue
        /// </summary>
        Task PublishAsync<T>(string queueName, T message) where T : class;

        /// <summary>
        /// Subscribes to queue messages
        /// </summary>
        Task SubscribeAsync<T>(string queueName, Func<T, Task> handler) where T : class;

        /// <summary>
        /// Creates queue if it doesn't exist
        /// </summary>
        Task CreateQueueIfNotExistsAsync(string queueName);
    }

    /// <summary>
    /// Infrastructure interface for email provider implementations.
    /// Technology-specific for SendGrid, SMTP, AWS SES, etc.
    /// </summary>
    public interface IEmailProvider
    {
        /// <summary>
        /// Sends email using the configured provider
        /// </summary>
        Task<bool> SendEmailAsync(string to, string subject, string htmlBody, string? textBody = null);

        /// <summary>
        /// Sends email with attachments
        /// </summary>
        Task<bool> SendEmailWithAttachmentsAsync(string to, string subject, string htmlBody, 
            IEnumerable<EmailAttachment> attachments);

        /// <summary>
        /// Validates email configuration
        /// </summary>
        Task<bool> ValidateConfigurationAsync();
    }

    /// <summary>
    /// Email attachment model for infrastructure layer
    /// </summary>
    public class EmailAttachment
    {
        public string FileName { get; set; } = null!;
        public byte[] Content { get; set; } = null!;
        public string ContentType { get; set; } = "application/octet-stream";
    }
}