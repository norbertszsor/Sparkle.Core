namespace Sparkle.Transfer
{
    public class PagedList<T>
    {
        public int TotalCount { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages - 1;

        public IEnumerable<T> Items { get; set; }

        public PagedList(IEnumerable<T> items, int count, int? pageIndex, int? pageSize)
        {
            TotalCount = count;
            PageIndex = pageIndex ?? 0;
            PageSize = pageSize ?? items.Count();
            Items = items;
        }
    }
}
