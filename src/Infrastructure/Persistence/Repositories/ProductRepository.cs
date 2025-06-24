using Dapper;
using ECommerce.Application.DTOs.Products;
using ECommerce.Domain;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.ValueObject.Products;
using ECommerce.Persistence.ECommerceDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq; 

namespace ECommerce.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly string _connectionString;


        public ProductRepository(EcommerceDbContext context, string connectionString)
        {
            _context = context;
            _connectionString = connectionString;


        }



        #region QUERYS
  
            public async Task<IReadOnlyList<Product>> GetAllAsyn()
            {
               return await _context.Set<Product>().ToListAsync();
               // return await _context.Products.ToListAsync();
            }

            public IQueryable<Product> GetAllQueryable()
            {
                return _context.Set<Product>();
            }

            

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

            public async Task<List<ProductResultDTO>> GetProductsFilterAsync(ProductFilterValue filter)
            {
                var query = _context.Products
                    .Include(p => p.IdCoinNavigation)
                    .AsNoTracking()
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(filter.Name))
                    query = query.Where(p => p.Name.Contains(filter.Name));

                if (filter.ProductTypeId.HasValue)
                    query = query.Where(p => p.IdCatProductType == filter.ProductTypeId);

                if (filter.CoinId.HasValue)
                    query = query.Where(p => p.IdCoin == filter.CoinId);

                if (filter.MinPrice.HasValue)
                    query = query.Where(p => p.Price >= filter.MinPrice);

                if (filter.MaxPrice.HasValue)
                    query = query.Where(p => p.Price <= filter.MaxPrice);

                return await query
                    .OrderBy(p => p.Name)
                    .Skip((filter.Page - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .Select(p => new ProductResultDTO
                    {
                        Name = p.Name,
                        Description = p.Description,
                        PriceCoin = $"{p.IdCoinNavigation.Symbol}{p.Price} {p.IdCoinNavigation.IsoCode}"
                    })
                    .ToListAsync();
            }

        #endregion


        #region filters dynamics or where
        public IQueryable<Product> GetByFilter(Expression<Func<Product, bool>> criterio)
            {
                return _context.Set<Product>().AsQueryable().Where(criterio);
            }

        #endregion



        #region JOINS
        public async Task<ProductResultDTO> GetProductJoinCoinById(int idProduct) 
        {
            IQueryable<Product> query = _context.Set<Product>()
                .Include(innerJoin => innerJoin.IdCoinNavigation)
                .AsNoTracking()
                .AsQueryable();


            return await query.Where(p => p.Id == idProduct)
                        .Select(p => new ProductResultDTO
                        {
                            Name = p.Name,
                            Description = p.Description,
                            PriceCoin = $"{p.IdCoinNavigation.Symbol}{p.Price} {p.IdCoinNavigation.IsoCode}"
                        })
                        .FirstOrDefaultAsync();
        }


        public IQueryable<Product> GetAllInlcudes()
        {
            IQueryable<Product> query = _context.Set<Product>()
                .Include(innerJoin => innerJoin.IdCoinNavigation)
                .ThenInclude(tblCoin => tblCoin.IdCoin);


            return query;
        }




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


        #region DAPPER
        public async Task<IReadOnlyList<Product>> GetResultFromDapper() 
        {
            using (var connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@PnIdProduct", dbType: DbType.Int32, value: null);
                parameters.Add("@PsName", dbType: DbType.String, value: "libro");
                parameters.Add("@PsDescription", dbType: DbType.String, value: null);
                parameters.Add("@PnPrice", dbType: DbType.Int32, value: null);
                parameters.Add("@PnIdCoin", dbType: DbType.Int32, value: null);
                parameters.Add("@PnIdCatProductType", dbType: DbType.Int32, value: null);
                parameters.Add("@PnActive", dbType: DbType.Int32, value: null);

                // Parámetros de salida
                parameters.Add("@PnEstatus", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@PsMensaje", dbType: DbType.String, size: 250, direction: ParameterDirection.Output);

                // Ejecutar procedimiento
                var productos = connection
                    .Query<Product>(
                        "Spc_Product",
                        param: parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                // Recuperar valores de salida
                int estatus = parameters.Get<int>("@PnEstatus");
                string mensaje = parameters.Get<string>("@PsMensaje");

                // Si quieres revisar
                Console.WriteLine($"Estatus: {estatus}, Mensaje: {mensaje}");
                return productos;
            }

        }
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
