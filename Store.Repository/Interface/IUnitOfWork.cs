using Store.data.Entity;

namespace Store.Repository.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        Task<int> Complete();
    }
}
