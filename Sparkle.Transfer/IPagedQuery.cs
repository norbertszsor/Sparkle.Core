namespace Sparkle.Transfer
{
    public interface IPagedQuery<out TResponse> : IQuery<TResponse>
    {
        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }
}
