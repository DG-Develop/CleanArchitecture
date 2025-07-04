using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using ECommerce.Persistence.ECommerceDbContext;

namespace ECommerce.Persistence.Repositories
{
    public class SalesRepository: GenericRepository<Sale>,  ISalesRepository
    {
        public SalesRepository(EcommerceDbContext context) : base(context)
        {
        }

    }
}
