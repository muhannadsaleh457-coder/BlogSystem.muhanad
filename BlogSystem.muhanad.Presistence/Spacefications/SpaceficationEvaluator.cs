using BlogSystem.muhanad.Domain.Contracts;
using BlogSystem.muhanad.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Presistence.Spacefications
{
    public static class SpaceficationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> context,ISpacefication<TEntity,TKey>? space) 
        {
            var query = context;

            if (space.Filteration is not null)
            {
              query =  query.Where(space.Filteration);
            }

            if (space.Page is not null && space.Size is not null)
            {
                query = query.Skip(space.Page.Value - 1 * space.Size.Value).Take(space.Size.Value);
            }

            if (space.Includes is not null)
            {
               query = space.Includes.Aggregate(query, (query, n) => query.Include(n));
            }

            return query;
        }
    }
}
