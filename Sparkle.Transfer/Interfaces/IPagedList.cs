namespace Sparkle.Transfer.Interfaces
{
    public interface IPagedList<T>
    {
        int TotalCount { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        IEnumerable<T> Items { get; set; }
    }
}
