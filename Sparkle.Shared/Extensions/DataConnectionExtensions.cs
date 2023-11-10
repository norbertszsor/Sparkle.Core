using LinqToDB;
using LinqToDB.Data;
using Sparkle.Shared.Helpers;

namespace Sparkle.Shared.Extensions
{
    public static class DataConnectionExtensions
    {
        public static async Task<string> InsertWithGuidIdentityAsync<T>(this DataConnection connection, T? entity)
            where T : class
        {
            var id = TokenHelper.GenerateToken();

            var idProperty = typeof(T).GetProperty("Id");

            if (idProperty == null || entity == null)
            {
                throw ThrowHelper.Throw<DataConnection>("Entity does not exist or do not have Id property.");
            }
 
            idProperty.SetValue(entity, id);
            
            await connection.InsertAsync(entity);

            return id;
        }

    }
}
