using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces
{
    /// <summary>
    /// Generic repository interface that defines the basic CRUD operations. (Create, Read, Update, Delete)
    /// for any entity in the context of the database.
    /// </summary>
    /// <typeparam name="T">T is the type entity of repository.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Obtain all the information about the entity of type T <typeparamref name="T"/>.
        /// </summary>
        /// <returns>read-only list of data.</returns>
        Task<IReadOnlyList<T>> GetAll();

        /// <summary>
        /// get information by id.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>Entity found or null if not found.</returns>
        Task<T?> GetById(int id);
        
        /// <summary>
        /// get information by id.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>Entity found or null if not found.</returns>
        IQueryable<T> GetByFilter(Expression<Func<T, bool>> criterio);





        /// <summary>
        /// add the new info at entity.
        /// </summary>
        /// <param name="entidad">The entity to be added.</param>
        /// <returns>The entity added.</returns>
        Task<T?> AddAsync(T entidad);

        /// <summary>
        /// Update info of the entity exist by id.
        /// </summary>
        /// <param name="entidad">The entity to be updated.</param>
        /// <returns>The entity updated.</returns>
        Task<T?> UpdateAsync(T entidad);

        /// <summary>
        /// Delete info of entity by id of the database.
        /// </summary>
        /// <param name="entidad">Entity to be eliminated.</param>
        /// <returns>Not return info.</returns>
        Task DeleteAsync(T entidad);
    }
}
