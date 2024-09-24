
using Microsoft.EntityFrameworkCore;

using Store.data.Context;
using Store.data.Entity;
using Store.Repository.Interface;

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

        public async Task<TEntity> GetByIdAsync(TKey id)
        => await _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);
    }
}
