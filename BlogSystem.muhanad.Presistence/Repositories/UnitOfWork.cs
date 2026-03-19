using BlogSystem.muhanad.Domain.Contracts;
using BlogSystem.muhanad.Domain.Entites;
using BlogSystem.muhanad.Presistence.Contexts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Presistence.Repositories
{
    public class UnitOfWork(BlogDbContext context) : IUnitOfWork
    {
        public IGenaricRepository<TEntity, TKey> GetGenaricRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {

            ConcurrentDictionary<string,object> dic = new ConcurrentDictionary<string,object>();

            return (IGenaricRepository<TEntity, TKey>)dic.GetOrAdd(typeof(TEntity).Name, new GenaricRepository<TEntity, TKey>(context));
        }

        public async Task<int> SaveChangesAsync()
        {
           return await context.SaveChangesAsync();
        }
    }
}
