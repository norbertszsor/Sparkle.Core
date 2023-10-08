using Sparkle.Shared.Helpers;
using Sparkle.Transfer;

namespace Sparkle.Shared.Extensions
{
    public static class EnumerableExtension
    {
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, dynamic query)
        {
            return source == null
                ? throw ThrowHelper.Throw<PagedList<T>>(nameof(source))
                : new PagedList<T>(source, source.Count(), query?.Page, query?.PageSize);
        }
    }
}
