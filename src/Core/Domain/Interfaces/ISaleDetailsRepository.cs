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
        Task DeleteLogicAsync(SaleDetail deleteSaleDetail);

        /// <summary>
        /// save of multiple SaleDetails
        /// </summary>
        /// <param name="idSale"></param>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        Task<int> SaveRangeAsync(List<SaleDetail> listSaleDetails);

        Task<List<SaleDetail>> GetSaleDetailsByIdVentaListIdProducts(List<int> idSaleVenta);

        Task<List<SaleDetail>> GetSaleDetailsByIdVentaListIdProducts(int idVenta, List<int> listIdProducts = null);

    }
}
