using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using ECommerce.Persistence.ECommerceDbContext;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Repositories
{
    public class SalesRepository: GenericRepository<Sale>,  ISalesRepository
    {
        public SalesRepository(EcommerceDbContext context) : base(context)
        {
        }

        public async Task<Sale> GetSaleDetails(int idSale)
        {
           return await _context.Sales.Include(x => x.SaleDetails).FirstOrDefaultAsync(x => x.IdSale == idSale);

        }


    }
}
