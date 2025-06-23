using ECommerce.Domain;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Persistence.ECommerceDbContext;

namespace ECommerce.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly EcommerceDbContext _context;
        
        public GenericRepository(EcommerceDbContext context)
        {
            _context = context;
        }


        #region Implementation of contract 

        public async Task<IReadOnlyList<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> criterio)
        {
            return _context.Set<TEntity>().AsQueryable().Where(criterio);
        }





        public async Task<TEntity?> AddAsync(TEntity entidad)
        {
            await _context.Set<TEntity>().AddAsync(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }


        public async Task DeleteAsync(TEntity entidad)
        {
                    _context.Remove(entidad);
            await _context.SaveChangesAsync();
        }

      
        public async Task<TEntity?> UpdateAsync(TEntity entidad)
        {
            _context.Entry(entidad).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entidad;
        }




        //Method extensions
        //Add range
        //Update range
        //Delete Range
        //Get by filter Inlcudes

        //public async Task<int> UpdateRange(IEnumerable<TEntity> entidades)
        //{
        //    _context.UpdateRange(entidades);
        //    await _context.SaveChangesAsync();
        //    return entidades.Count();
        //}




        public void Dispose()
            {
                _context.Dispose();
            }
        #endregion

    }


}
