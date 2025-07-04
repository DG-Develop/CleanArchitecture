namespace ECommerce.Application.Features.SalesAgrregate.Dtos.Request
{
    public class SalesParametersDto
    {
        public string Date { get; set; } = string.Empty;
        public int ProductId { get; set; }
        // n cantidad de atributos que se necesiten para filtrar las ventas
    }
}
