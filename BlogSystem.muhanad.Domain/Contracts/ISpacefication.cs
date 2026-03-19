using BlogSystem.muhanad.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Domain.Contracts
{
    public interface ISpacefication<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public List<Expression<Func<TEntity, object>>>? Includes { get; set; }
        public Expression<Func<TEntity, bool>>? Filteration { get; set; }
        public int? Size { get; set; }
        public int? Page { get; set; }
    }
}
