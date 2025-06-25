using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using ECommerce.Persistence.ECommerceDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repositories
{
    public class CatProducsTypeRepository : GenericRepository<CatProductsType>, ICatProductsType
    {
        public CatProducsTypeRepository(EcommerceDbContext context) : base(context)
        {
        }


        public void GetReport()
        {
            
            var catalogo = GetById(2);

            throw new NotImplementedException();
        }
    }
}
