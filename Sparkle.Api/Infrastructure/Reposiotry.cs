using LinqToDB;
using Sparkle.Api.Data;
using Sparkle.Api.Domain;

namespace Sparkle.Api.Infrastructure
{
    public class Reposiotry<TEntity, TKey> : 
        IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly SparkleContext _dataContext;

        public Reposiotry(SparkleContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dataContext.GetTable<TEntity>().FirstOrDefaultAsync(e => e.Equals(id));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dataContext.GetTable<TEntity>().ToListAsync();
        }

        public async Task<string> AddAsync(TEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;

            return (string)await _dataContext.InsertWithIdentityAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            await _dataContext.UpdateAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _dataContext.DeleteAsync(entity);
        }
    }
}
