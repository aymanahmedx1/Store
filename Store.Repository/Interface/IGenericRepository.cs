using Store.data.Entity;

namespace Store.Repository.Interface
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> GetByIdAsync(TKey id);
        public Task<IReadOnlyList<TEntity>> GetAllAsync();
    }
}
