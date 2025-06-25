using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.ValueObject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    public interface IProductRepository
    {

        #region queryable and not queryable

        Task<List<Product>> getPagination(int NumPage, int TotalPage, int? FilterProductType, int take);

        Task<IReadOnlyList<Product>> GetAllAsyn();

        IQueryable<Product> GetAllQueryable();

        #endregion

        //join
        Task<ProductResultDTO> GetProductJoinCoinById(int idProduct);


        //Filters dynamics or where
        IQueryable<Product> GetByFilter(Expression<Func<Product, bool>> criterio);



        //JOINS
        IQueryable<Product> GetByFilterInclude(Expression<Func<Product, bool>> criterio, params Expression<Func<Product, object>>[] includes);

        Task<List<ProductResultDTO>> GetProductsFilterAsync(ProductFilterValue filter);

        Task<IReadOnlyList<ProductDetailValueJoin>> GetResultFromSQL();

        Task<Product?> AddAsync(Product product);


        //SP's
        Task<IReadOnlyList<Product>> GetResultFromDapper();

        #region not implemented
        Task<Product?> GetByIdAsync(int id);
        Task UpdateAsync(Product product);

        #endregion


    }
}
