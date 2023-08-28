using Sparkle.Api.Domain;

namespace Sparkle.Api.Data.Interfaces
{
    public interface IReposiotry<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        Task<TEntity?> GetByIdAsync(TKey id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<string> AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
