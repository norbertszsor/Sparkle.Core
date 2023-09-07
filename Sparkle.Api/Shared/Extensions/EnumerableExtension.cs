using Sparkle.Api.Shared.Helpers;
using Sparkle.Transfer;

namespace Sparkle.Api.Shared.Extensions
{
    public static class EnumerableExtension
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, dynamic query)
        {
            if(source == null)
                throw ThrowHelper.Throw<SparkleException>(nameof(source));

            return new PagedList<T>(source, source.Count(), query?.Page, query?.PageSize);
        }
    }
}
