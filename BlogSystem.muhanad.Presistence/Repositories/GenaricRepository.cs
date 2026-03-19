using BlogSystem.muhanad.Domain.Contracts;
using BlogSystem.muhanad.Domain.Entites;
using BlogSystem.muhanad.Presistence.Contexts;
using BlogSystem.muhanad.Presistence.Spacefications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Presistence.Repositories
{
    public class GenaricRepository<TEntity, TKey>(BlogDbContext _context) : IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>>? GetAllAsync(ISpacefication<TEntity,TKey> spac)
        {

            return await SpaceficationEvaluator<TEntity,TKey>.GetQuery(_context.Set<TEntity>(), spac).ToListAsync();
            
        }


        public async Task<TEntity>? GetByIdAsync(ISpacefication<TEntity, TKey> spac)
        {
            return await SpaceficationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spac).FirstOrDefaultAsync();
        }

        public async Task<TEntity>? GetByIdAsync(TKey Id)
        {
            return await _context.Set<TEntity>().FindAsync(Id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

    
    }
}
