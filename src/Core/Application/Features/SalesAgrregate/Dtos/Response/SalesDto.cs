namespace ECommerce.Application.Features.SalesAgrregate.Dtos.Response
{
    public class SalesDto
    {
        public int SaleId { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }
        public string DescriptionPayment { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
    }
}
