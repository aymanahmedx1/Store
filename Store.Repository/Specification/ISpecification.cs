using System.Linq.Expressions;

namespace Store.Repository.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>> Includes { get; }

        Expression<Func<T, object>> Orderby { get; }
        Expression<Func<T, object>> OrderbyDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPaginated {  get; }

    }
}
