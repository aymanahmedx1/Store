using System.Linq;

using Microsoft.EntityFrameworkCore;

using Store.data.Entity;

namespace Store.Repository.Specification
{
    public class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specs)
        {

            var query = inputQuery;
            if (specs.Criteria is not null)
                query = query.Where(specs.Criteria);

            if (specs.Orderby is not null)
                query = query.OrderBy(specs.Orderby);

            if (specs.OrderbyDescending is not null)
                query = query.OrderByDescending(specs.OrderbyDescending);

            if (specs.IsPaginated)
                query = query.Skip(specs.Skip).Take(specs.Take);

            query = specs.Includes.Aggregate(query,
                (currentQuery, newInclude) => currentQuery.Include(newInclude));

            return query;
        }
    }
}
