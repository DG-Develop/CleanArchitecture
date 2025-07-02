using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using ECommerce.Persistence.ECommerceDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repositories
{
    public class SaleRepository: GenericRepository<Sale>, ISaleRepository
    {

        private readonly EcommerceDbContext _context;

        public SaleRepository(EcommerceDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task DeleteLogicSale(int idSale)
        {
            var sales = await _context.Sales.Include(x => x.SaleDetails).FirstOrDefaultAsync(x => x.IdSale == idSale);
            sales.Active = false;
            sales.SaleDetails.FirstOrDefault(x => x.Active = false);
            await _context.SaveChangesAsync();
        }

        public async Task<Sale> GetSaleAndDetails(int idSale)
        {
            return await _context.Sales.Include(x => x.SaleDetails).FirstOrDefaultAsync(x => x.IdSale == idSale);
        }


        public async Task saveChangeAsync()
        {
             await _context.SaveChangesAsync();
        }

    }
}
