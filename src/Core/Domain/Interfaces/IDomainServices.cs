namespace ECommerce.Domain.Interfaces
{
    /// <summary>
    /// Domain service interface for business logic that doesn't naturally fit in entities.
    /// This belongs in Domain because it contains core business rules and calculations.
    /// </summary>
    public interface IPriceCalculatorService
    {
        /// <summary>
        /// Calculates the final price for a product considering discounts, taxes, etc.
        /// This is pure business logic that belongs in the domain.
        /// </summary>
        decimal CalculateFinalPrice(Product product, CustomerType customerType, int quantity);

        /// <summary>
        /// Determines if a price change is significant enough to require approval.
        /// Business rule that belongs in domain.
        /// </summary>
        bool RequiresApprovalForPriceChange(decimal oldPrice, decimal newPrice);

        /// <summary>
        /// Calculates bulk discount based on business rules.
        /// Pure domain logic.
        /// </summary>
        decimal CalculateBulkDiscount(decimal unitPrice, int quantity);
    }

    /// <summary>
    /// Domain service for product validation business rules.
    /// Contains domain-specific validation that goes beyond simple property validation.
    /// </summary>
    public interface IProductDomainValidationService
    {
        /// <summary>
        /// Validates if product name follows business naming conventions.
        /// Domain business rule.
        /// </summary>
        bool IsValidProductName(string name, int categoryId);

        /// <summary>
        /// Checks if product can be created based on business constraints.
        /// Domain logic for business rules.
        /// </summary>
        Task<bool> CanCreateProductAsync(Product product);

        /// <summary>
        /// Validates price according to business rules (minimum margins, competitor pricing, etc.)
        /// Pure domain validation logic.
        /// </summary>
        bool IsValidPrice(decimal price, int categoryId, int coinId);
    }

    /// <summary>
    /// Enum for customer types - domain concept
    /// </summary>
    public enum CustomerType
    {
        Regular,
        Premium,
        Wholesale,
        Enterprise
    }
}