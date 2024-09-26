
using Microsoft.EntityFrameworkCore;

using Store.data.Context;
using Store.data.Entity;
using Store.Repository.Interface;
using Store.Repository.Specification;

namespace Store.Repository.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);
        public void Delete(TEntity entity)
             => _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        => await _context.Set<TEntity>().ToListAsync();

        public void Update(TEntity entity)
       => _context.Set<TEntity>().Update(entity);


        public async Task<TEntity> GetByIdAsync(TKey id)
        => await _context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> specs)
        => await getFullQuery(specs).FirstOrDefaultAsync();
        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs)
         => await getFullQuery(specs).ToListAsync();
        public async Task<int> GetCountWithSpecificationAsync(ISpecification<TEntity> specs)
         => await getFullQuery(specs).CountAsync();
        private IQueryable<TEntity> getFullQuery(ISpecification<TEntity> specs)
            => SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specs);


    }
}
