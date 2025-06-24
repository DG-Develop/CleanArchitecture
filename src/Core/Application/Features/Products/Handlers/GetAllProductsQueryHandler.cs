using ECommerce.Application.Features.Products.Queries;
using ECommerce.Domain;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.ValueObject.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;


namespace ECommerce.Application.Features.Products.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            //Querys e IQueryables 

            //memory
            Console.WriteLine($"/**************************************************/");
            Console.WriteLine($"                                                   ");
            IReadOnlyList<Product> listProducts =  await _repository.GetAllAsyn();
            Console.WriteLine($"listProducts FINISH");
            Console.WriteLine($"/**************************************************/");


            //memory
            Console.WriteLine($"/**************************************************/");
            Console.WriteLine($"                                                   ");
            IQueryable<Product> productQuery =  _repository.GetAllQueryable();
            List<Product> filteredProducts =  productQuery.ToList();
            
            Console.WriteLine($"filteredProductsAsQuery FINISH");
            Console.WriteLine($"/**************************************************/");




            //GetByFilter
            Console.WriteLine($"/**************************************************/");
            Console.WriteLine($"                                                   ");
            IQueryable<Product> GetByFilter =  _repository.GetByFilter(p => p.Price > 30);
            var filteredProductsByFilter = GetByFilter.ToList();
            Console.WriteLine($"GetByFilter FINISH");
            Console.WriteLine($"/**************************************************/");


            Console.WriteLine($"/**************************************************/");
            Console.WriteLine($"                                                    ");
            int idProduct = 5;
            ProductResultDTO productResult = await _repository.GetProductJoinCoinById(idProduct);
            Console.WriteLine($"JOIN FINISH");
            Console.WriteLine($"/**************************************************/");



            /******************************************************************************/
            //                      Eager Loading && Lazy Loading
            /******************************************************************************/

            ////GetByFilterInclude
            //Console.WriteLine($"/**************************************************/");
            //Console.WriteLine($"( Eager Loading )");
            //IQueryable<Product> productQueryIncludes = _repository.GetByFilterInclude(p => p.Price > 30, include => include.IdCoinNavigation);
            //var productsWithIncludes = productQueryIncludes.ToList();
            //Console.WriteLine($"/**************************************************/");


            //Console.WriteLine($"/**************************************************/");
            //Console.WriteLine($"Lazy Loading");
            //IQueryable<Product> productQueryLazy = _repository.GetAllQueryable();
            //List<Product> listProductTop10_Lazy = productQueryLazy.OrderByDescending(x => x.Id).Take(10).ToList();

            //string? nameCoin = listProductTop10_Lazy[0].IdCoinNavigation?.Name;
            //Console.WriteLine($"Lazy Loading - NAME: {nameCoin}");
            //Console.WriteLine($"/**************************************************/");


            /******************************************************************************/
            //                            FromSQL and Interpolated Strings
            /******************************************************************************/
            IReadOnlyList<ProductDetailValueJoin> readOnlylistFromSql = await _repository.GetResultFromSQL();



            IReadOnlyList<Product> readOnlyListProductFromSql = await _repository.GetResultFromDapper();



            //return await _repository.GetAllAsync();
            return filteredProducts;
        }
    }
}
