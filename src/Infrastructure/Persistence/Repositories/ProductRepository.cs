using ECommerce.Application.DTOs.Products;
using ECommerce.Domain;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.ValueObject.Products;
using ECommerce.Persistence.ECommerceDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq; 

namespace ECommerce.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _context;

        public ProductRepository(EcommerceDbContext context)
        {
            _context = context;
        }



        #region QUERYS
            public async Task<List<Product>> getPagination(int NumPage, int TotalPage, int? FilterProductType, int take = 10)
            {
                var query = _context.Products.AsQueryable();

                if (FilterProductType.HasValue)
                {
                    query = query.Where(p => p.IdCatProductType == FilterProductType);
                }

                int y = (NumPage - 1) * TotalPage;

                var listado = await query
                    .Skip((NumPage - 1) * TotalPage)
                    .Take(take)
                    .ToListAsync();

                return listado;
            }

            public async Task<IReadOnlyList<Product>> GetAllAsyn()
            {
               return await _context.Set<Product>().ToListAsync();
               // return await _context.Products.ToListAsync();
            }

            public IQueryable<Product> GetAllQueryable()
            {
                return _context.Set<Product>();
            }

        #endregion


        #region filters dynamics or where
            public IQueryable<Product> GetByFilter(Expression<Func<Product, bool>> criterio)
            {
                return _context.Set<Product>().AsQueryable().Where(criterio);
            }

        #endregion



        #region JOINS

        // Eagle Loading
        public IQueryable<Product> GetByFilterInclude(Expression<Func<Product, bool>> criterio, params Expression<Func<Product, object>>[] includes)
        {
            IQueryable<Product> query = _context.Set<Product>();

            // Aplicar los Includes si es necesario
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Retornar el IQueryable con los filtros aplicados
            return query.Where(criterio);
        }


        public IQueryable<Product> GetAllInlcudes()
        {
            IQueryable<Product> query = _context.Set<Product>()
                .Include(innerJoin  => innerJoin.IdCoinNavigation)
                .ThenInclude(inner => inner.IdCoin);

         
            return query;
        }


        //Lazy Loading
        //public void GetTop10_LazyLoading()
        //{
        //    Console.WriteLine("                           ");
        //    Console.WriteLine("GetTop10_LazyLoading");

        //    IQueryable<Product> query = _context.Set<Product>();
        //    List<Product> listProductTop10 = query.OrderByDescending(x => x.Id).Take(10).ToList();

        //    string? nameCoin = listProductTop10[0].IdCoinNavigation?.Name;

        //}

        public async Task<IReadOnlyList<ProductDetailValueJoin>> GetResultFromSQL()
        {
            string query = @"select [p].[Name],
                                    [p].[Description], 
                                    [p].[Price] 'Price',
                                    CONCAT([Coin].[Symbol], [p].[Price] , ' ',  [Coin].[IsoCode] ) 'priceCoin',
                                    [Ptype].[Descrip] 
                            from [dbo].[Products] as P
                            inner join [dbo].[Cat_ProductsType] as [Ptype]
                            ON [p].[IdCatProductType] = [Ptype].[IdCatProductType]
                            inner join [dbo].[Cat_Coins] as Coin
                            ON [p].[IdCoin] = Coin.[IdCoin]";


            return await _context.ProductDTO.FromSqlRaw(query)
                                         .AsNoTracking().ToListAsync();
        }


        //public async Task<IReadOnlyList<ProductDetailValueJoin>> GetResultFromSQLInterpolated()
        //{

        //}



        #endregion







        public async Task<Product?> AddAsync(Product entidad)
        {
            await _context.Set<Product>().AddAsync(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }


        #region Not Implemented
        public Task<Product?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
