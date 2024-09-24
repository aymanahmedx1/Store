using System.Collections;

using Store.data.Context;
using Store.data.Entity;
using Store.Repository.Interface;

namespace Store.Repository.Repository
{
    public class UnitOfWork(ApplicationDbContext _context) : IUnitOfWork
    {
        private Hashtable _repository;
        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (_repository is null)
                _repository = new Hashtable();

            var entityKey = typeof(TEntity).Name;
            if (!_repository.ContainsKey(entityKey))
            {
                var repositoryType = typeof(GenericRepository<,>);
                var repositoryInstance = Activator.CreateInstance
                    (repositoryType.MakeGenericType
                    (typeof(TEntity), typeof(TKey)), _context);
                _repository.Add(entityKey, repositoryInstance);
            }
            return (IGenericRepository<TEntity, TKey>)_repository[entityKey];
        }
        public async Task<int> Complete()
        => await _context.SaveChangesAsync();


    }
}
