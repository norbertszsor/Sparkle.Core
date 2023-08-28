using LinqToDB;
using LinqToDB.Data;

namespace Sparkle.Api.Shared.Extensions
{
    public static class DataConnectionExtensions
    {
        public static async Task<string> InsertWithGuidIdentityAsync<T>(this DataConnection connection, T entity) 
            where T : class
        {
            var id = Guid.NewGuid().ToString();

            var idProperty = typeof(T).GetProperty("Id");

            if (idProperty != null && entity != null)
            {
                idProperty.SetValue(entity, id);
            }
            else
            {
                throw new SparkleException("Entity does not exist or do not have Id property.");
            }

            await connection.InsertAsync(entity);

            return id;
        }

    }
}
