using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Domain.Features.Products
{
    public interface IProductRepository
    {
        Task<int> AddAsync(Product product);
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
