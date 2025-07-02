using ECommerce.Domain.EcommerceDbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface ISaleDetailsRepository  : IGenericRepository<SaleDetail>
    {
        /// <summary>
        /// Logic eliminated
        /// </summary>
        /// <param name="entidad">Entity to be eliminated.</param>
        /// <returns>Not return info.</returns>
        Task DeleteLogicSaleDetailsAsync(List<int> idsSaleDetails);

        /// <summary>
        /// save of multiple SaleDetails
        /// </summary>
        /// <param name="idSale"></param>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        Task<int> SaveRangeAsync(List<SaleDetail> listSaleDetails);

        Task<List<SaleDetail>> GetSaleDetailsByIdSaleDetails(List<int> idSaleDetails, bool AsTracking = false);

        Task<List<SaleDetail>> GetSaleDetailsByIdVentaListIdProducts(int idSale, List<int> listIdProducts = null, bool AsTracking = false);

    }
}
