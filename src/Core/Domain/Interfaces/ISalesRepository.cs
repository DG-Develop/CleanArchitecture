using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.EcommerceDbEntities;

namespace ECommerce.Domain.Interfaces
{
    public interface ISalesRepository: IGenericRepository<Sale>
    {

        Task<Sale> GetSaleDetails(int idSale);
        
    }
}
