using LinqToDB;
using Sparkle.Domain.Data;
using Sparkle.Domain.Interfaces;
using Sparkle.Shared.Extensions;
using Sparkle.Shared.Helpers;

namespace Sparkle.Infrastructure.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        private readonly SparkleContext _storage;

        public Repository(SparkleContext context)
        {
            _storage = context;
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _storage.GetTable<TEntity>()
                .FirstOrDefaultAsync(e => e.Id != null && e.Id.Equals(id));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _storage.GetTable<TEntity>()
                .ToListAsync();
        }

        public async Task<bool> ExistAsync(TKey id)
        {
            return await _storage.GetTable<TEntity>()
                .AnyAsync(e => e.Id != null && e.Id.Equals(id));
        }

        public async Task EnsureExistAsync(TKey id)
        {
            if (!await ExistAsync(id))
            {
                throw ThrowHelper.Throw<Repository<TEntity, TKey>>($"{typeof(TEntity).Name} with id {id} not found");
            }
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
