using AutoMapper;
using ECommerce.Application.Features.SalesAgrregate.Commands;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class CreateDetailSaleCommandHandler : IRequestHandler<CreateDetailSaleCommand, int>
    {
        private readonly IMapper _mapper;

        public CreateDetailSaleCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<int> Handle(CreateDetailSaleCommand request, CancellationToken cancellationToken)
        {
            // Obtener precio del  Producto por ProductId

            // Calcular Precio Total (Precio * Cantidad)
            //var total =  request.Amount * precioOrigen
            //var detailSale = _mapper.Map<SaleDetail>(request);
            //detailSale.Total = total
            //deatilSale.PresioOrigen = producto.precioOrigen;

            //AddASync

            throw new NotImplementedException();
        }
    }
}
