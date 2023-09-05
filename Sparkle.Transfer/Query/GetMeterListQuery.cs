using Sparkle.Transfer.Data;

namespace Sparkle.Transfer.Query
{
    public class GetMeterListQuery : IPagedQuery<PagedList<MeterDto>>
    {
        public required string CompanyId { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }
}
