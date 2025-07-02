using ECommerce.Domain.EcommerceDbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface ISaleRepository: IGenericRepository<Sale>
    {
        Task DeleteLogicSale(int idSale);

        Task<Sale> GetSaleAndDetails(int idSale);

        Task saveChangeAsync();
    }
}
