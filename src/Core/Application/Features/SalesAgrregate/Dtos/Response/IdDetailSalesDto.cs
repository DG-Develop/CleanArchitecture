namespace ECommerce.Application.Features.SalesAgrregate.Dtos.Response
{
    public class IdDetailSalesDto : SalesDto
    {
        public List<DetailSalesDto> Details { get; set; } = new List<DetailSalesDto>();
    }
}
