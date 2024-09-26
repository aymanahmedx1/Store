using System.Linq.Expressions;

namespace Store.Repository.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> Orderby { get; private set; }

        public Expression<Func<T, object>> OrderbyDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void AddInclud(Expression<Func<T, object>> include)
            => Includes.Add(include);

        protected void AddOrderBy(Expression<Func<T, object>> order)
           => Orderby = order;
        protected void AddOrderByDescending(Expression<Func<T, object>> order)
         => OrderbyDescending = order;

        protected void ApplyPagination(int skip, int take) { 
            
            Take = take;    
            Skip = skip;
            IsPaginated = true; 
        }

    }
}
