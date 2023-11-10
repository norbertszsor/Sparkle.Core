namespace Sparkle.Domain.Interfaces
{
    public interface IRepository<TEntity, in TKey> where TEntity : class, IEntity<TKey>
    {
        Task<TEntity?> GetByIdAsync(TKey id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<bool> ExistAsync(TKey id);

        Task EnsureExistAsync(TKey id);

        Task<string> AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
