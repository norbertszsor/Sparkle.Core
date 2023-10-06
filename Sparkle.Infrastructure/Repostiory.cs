using LinqToDB;
using Sparkle.Domain;
using Sparkle.Domain.Data;
using Sparkle.Domain.Interfaces;
using Sparkle.Shared.Extensions;

namespace Sparkle.Infrastructure
{
    public class Repostiory<TEntity, TKey> :
        IReposiotry<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly SparkleContext _storage;

        public Repostiory(SparkleContext context)
        {
            _storage = context;
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _storage.GetTable<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(id));
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
