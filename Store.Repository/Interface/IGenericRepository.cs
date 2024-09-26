using Store.data.Entity;
using Store.Repository.Specification;

namespace Store.Repository.Interface
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity> GetByIdAsync(TKey id);
        public Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> specs);
        public Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs);
        public Task<int> GetCountWithSpecificationAsync(ISpecification<TEntity> specs);
    }
}
