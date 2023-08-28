using Sparkle.Transfer.Data;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Sparkle.Transfer.Query
{
    public class GetPredictionQuery : IQuery<PredictionDto>
    {
        public required string MeterName { get; set; }

        public required int Hours { get; set; }

        public static bool TryParse(string queryString, out GetPredictionQuery? query)
        {
            query = null;

            if (string.IsNullOrWhiteSpace(queryString))
            {
                return false;
            }

            var queryDictionary = HttpUtility.ParseQueryString(queryString);

            if (queryDictionary.Count == 0)
            {
                return false;
            }

            query = new GetPredictionQuery
            {
                MeterName = queryDictionary[nameof(MeterName)]?.ToString() ?? "",
                Hours = int.TryParse(queryDictionary[nameof(Hours)]?.ToString(), out var hours) ? hours : 0,
            };

            return true;
        }
    }
}
