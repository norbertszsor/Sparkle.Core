namespace Sparkle.Domain.Interfaces
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
