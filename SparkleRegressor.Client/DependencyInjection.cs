using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SparkleRegressor.Client.Abstraction;
using SparkleRegressor.Client.Configuration;
using SparkleRegressor.Client.Logic;

namespace SparkleRegressor.Client
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSparkleRegressorClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            var srcSettings = configuration.GetSection(nameof(SrcSettings)).Get<SrcSettings>() ??
                              throw new Exception($"Can't get {nameof(SrcSettings)} from configuration");

            ConfigureSparkleRegressorHttpClient(services, srcSettings);

            return services;
        }

        public static IServiceCollection AddSparkleRegressorClient(this IServiceCollection services,
            SrcSettings srcSettings)
        {
            if (srcSettings is null) throw new ArgumentNullException(nameof(srcSettings));

            ConfigureSparkleRegressorHttpClient(services, srcSettings);

            return services;
        }

        private static void ConfigureSparkleRegressorHttpClient(IServiceCollection services, SrcSettings srcSettings)
        {
            if(string.IsNullOrWhiteSpace(srcSettings.AccessToken))
                srcSettings.AccessToken = Environment.GetEnvironmentVariable("SRC_ACCESS_TOKEN");

            services.AddHttpClient<ISparkleRegressorClient, SparkleRegressorClient>((_,
                    client) =>
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Api-Key", srcSettings.AccessToken);
                    client.BaseAddress = new Uri(srcSettings.BaseUrl);
                    client.Timeout = TimeSpan.FromMinutes(10);
                })
                .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(10),
                }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
        }
    }
}
