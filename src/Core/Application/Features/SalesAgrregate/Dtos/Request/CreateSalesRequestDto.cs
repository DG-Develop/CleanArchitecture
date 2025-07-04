namespace ECommerce.Application.Features.SalesAgrregate.Dtos.Request
{
    public class CreateSalesRequestDto
    {
        public int Amount { get; set; }
        public decimal Total { get; set; }
        public int PaymentTypeId { get; set; }
    }
}
