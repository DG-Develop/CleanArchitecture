namespace ECommerce.Application.Features.SalesAgrregate.Dtos.Response
{
    public class DetailSalesDto
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal OriginPrice { get; set; }
        public decimal Total { get; set; }
    }
}
