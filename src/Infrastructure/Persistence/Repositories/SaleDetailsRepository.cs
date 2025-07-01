using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using ECommerce.Persistence.ECommerceDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;


namespace ECommerce.Persistence.Repositories
{
    public class SaleDetailsRepository  : GenericRepository<SaleDetail>, ISaleDetailsRepository
    {
        private readonly EcommerceDbContext _context;

        public SaleDetailsRepository(EcommerceDbContext context) : base(context)
        {
            _context = context;
        }
        
        
        /// <summary>
        /// Logic eliminated
        /// </summary>
        /// <param name="entidad">Entity to be eliminated.</param>
        /// <returns>Not return info.</returns>
        public async Task DeleteLogicAsync(SaleDetail deleteSaleDetail)
        {
            deleteSaleDetail.Active = false;
            _context.Entry(deleteSaleDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// save of multiple SaleDetails
        /// </summary>
        /// <param name="listSaleDetails"></param>
        /// <returns>total of listSaleDetails</returns>
        public async Task<int> SaveRangeAsync(List<SaleDetail> listSaleDetails)
        {
            _context.SaleDetails.AddRange(listSaleDetails);
            await _context.SaveChangesAsync();
            return listSaleDetails.Count;
        }



        /// <summary>
        /// update of multiple SaleDetails
        /// </summary>
        /// <param name="listSaleDetails"></param>
        /// <returns>total of listSaleDetails</returns>
        public async Task<int> UpdateRangeAsync(List<SaleDetail> listSaleDetails)
        {
            _context.SaleDetails.UpdateRange(listSaleDetails);
            await _context.SaveChangesAsync();
            return listSaleDetails.Count;
        }


        public async Task<List<SaleDetail>> GetSaleDetailsByIdSaleDetails(List<int> idSaleDetails, bool AsTracking = false) 
        {
            IQueryable<SaleDetail> querySaleDetails = _context.SaleDetails.AsQueryable();
            if (!AsTracking)
            {
                querySaleDetails = querySaleDetails.AsNoTracking();
            }

            querySaleDetails = querySaleDetails.Where(x => idSaleDetails.Contains(x.IdSaleDetails) );
            return await querySaleDetails.ToListAsync();
        }


        public async Task<List<SaleDetail>> GetSaleDetailsByIdVentaListIdProducts(int idSale, List<int> listIdProducts = null , bool AsTracking = false)
        {
            IQueryable<SaleDetail> querySaleDetails = _context.SaleDetails.AsQueryable();

            // If AsTracking is false, we use AsNoTracking to improve performance
            if (!AsTracking)
            {
                querySaleDetails = querySaleDetails.AsNoTracking();
            }

            if (idSale > 0 && listIdProducts == null) 
            {
                // Filter by IdSale
                querySaleDetails = querySaleDetails.Where(x => x.IdSale == idSale);
            }


            if (listIdProducts != null )
            {
                if (listIdProducts.Count > 1) 
                {
                    querySaleDetails = querySaleDetails.Where(x => listIdProducts.Contains(x.IdProduct));
                }
                else 
                {
                    // If listIdProducts is null or has only one element, we can skip this filter
                    querySaleDetails = querySaleDetails.Where(x => x.IdProduct == listIdProducts.FirstOrDefault());
                }
            }

            return await querySaleDetails.ToListAsync();
        }

    }
   
    
}
