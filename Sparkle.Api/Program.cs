using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Sparkle.Api.Data;
using Sparkle.Api.Data.Interfaces;
using Sparkle.Api.Domain.Models;
using Sparkle.Api.Infrastructure;
using Sparkle.Api.Presentation.Endpoints;
using Sparkle.Api.Presentation.ErrorHandler;
using Sparkle.Api.Shared.Extensions;
using SparkleRegressor.Client;
using SparkleRegressor.Client.Abstraction;
using SparkleRegressor.Client.Logic;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default") ?? 
    throw new Exception("No connection string found");

builder.Services.AddLogging(options =>
{
    options.AddConsole();
    options.AddDebug();
});

builder.Services.AddTransient<ErrorHandlingMiddleware>();

builder.Services.AddLinqToDBContext<SparkleContext>((provider, options)
    => options.UseSQLite(connectionString).UseDefaultLogging(provider));

builder.Services.AddSingleton<ISeeder, Seeder>();

builder.Services.AddScoped<IReposiotry<MeterEm, string?>, Repostiory<MeterEm, string?>>();
builder.Services.AddScoped<IReposiotry<CompanyEm, string?>, Repostiory<CompanyEm, string?>>();
builder.Services.AddScoped<IReposiotry<ReadingEm, string?>, Repostiory<ReadingEm, string? >>();

builder.Services.AddMediatR(options => 
{
    options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    options.Lifetime = ServiceLifetime.Scoped;
});

builder.Services.AddHttpClient<ISparkleRegressorClient, SparkleRegressorClient>((serviceProvider, client) =>
{
    var srcSettings = builder.Configuration.GetSection(nameof(SRCSettings)).Get<SRCSettings>() ?? 
        throw new Exception("No SRC settings found");

    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.BaseAddress = new Uri(srcSettings.BaseUrl);

}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new SocketsHttpHandler
    {
        PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
    };
}).SetHandlerLifetime(Timeout.InfiniteTimeSpan);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapReggressorEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RunMigrator();

app.RunSeeder();

app.Run();