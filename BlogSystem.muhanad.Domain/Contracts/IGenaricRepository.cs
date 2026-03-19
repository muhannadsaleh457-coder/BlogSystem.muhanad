using BlogSystem.muhanad.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Domain.Contracts
{
    public interface IGenaricRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>>? GetAllAsync(ISpacefication<TEntity, TKey> spac);
        Task<TEntity>? GetByIdAsync(ISpacefication<TEntity, TKey> spac);
        Task<TEntity>? GetByIdAsync(TKey Id);
        Task AddAsync(TEntity entity);
         void Update(TEntity entity);
         void Delete(TEntity entity);

    }
}
