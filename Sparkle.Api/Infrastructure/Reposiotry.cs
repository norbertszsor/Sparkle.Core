using LinqToDB;
using Sparkle.Api.Data;
using Sparkle.Api.Data.Interfaces;
using Sparkle.Api.Domain;
using Sparkle.Api.Shared.Extensions;

namespace Sparkle.Api.Infrastructure
{
    public class Reposiotry<TEntity, TKey> : 
        IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly SparkleContext _storage;

        public Reposiotry(SparkleContext dataContext)
        {
            _storage = dataContext;
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _storage.GetTable<TEntity>().FirstOrDefaultAsync(e => e.Equals(id));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _storage.GetTable<TEntity>().ToListAsync();
        }

        public async Task<string> AddAsync(TEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;

            return await _storage.InsertWithGuidIdentityAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            await _storage.UpdateAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _storage.DeleteAsync(entity);
        }
    }
}
