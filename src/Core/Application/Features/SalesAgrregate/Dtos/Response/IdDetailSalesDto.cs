namespace ECommerce.Application.Features.SalesAgrregate.Dtos.Response
{
    public class IdDetailSalesDto : SalesDto
    {
        public List<DetailSalesDto> DetailSales { get; set; } = new List<DetailSalesDto>();
    }
}
