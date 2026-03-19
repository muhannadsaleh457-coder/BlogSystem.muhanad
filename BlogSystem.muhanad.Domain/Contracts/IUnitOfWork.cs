using BlogSystem.muhanad.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Domain.Contracts
{
    public interface IUnitOfWork
    {

        IGenaricRepository<TEntity, TKey> GetGenaricRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        Task<int> SaveChangesAsync();
    }
}
