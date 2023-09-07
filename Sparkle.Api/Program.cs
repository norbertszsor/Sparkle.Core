using FluentValidation;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Sparkle.Api.Data;
using Sparkle.Api.Data.Interfaces;
using Sparkle.Api.Domain.Models;
using Sparkle.Api.Infrastructure;
using Sparkle.Api.Presentation;
using Sparkle.Api.Presentation.Endpoints;
using Sparkle.Api.Presentation.Validation;
using Sparkle.Api.Shared.Extensions;
using SparkleRegressor.Client;
using SparkleRegressor.Client.Abstraction;
using SparkleRegressor.Client.Logic;
using System.Reflection;

#region builder
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

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    options.Lifetime = ServiceLifetime.Scoped;
});

#region repositories
builder.Services.AddScoped<IReposiotry<MeterEm, string?>, Repostiory<MeterEm, string?>>();
builder.Services.AddScoped<IReposiotry<CompanyEm, string?>, Repostiory<CompanyEm, string?>>();
builder.Services.AddScoped<IReposiotry<ReadingEm, string?>, Repostiory<ReadingEm, string? >>();
#endregion

#region http clients
builder.Services.AddHttpClient<ISparkleRegressorClient, SparkleRegressorClient>((serviceProvider, client) =>
{
    var srcSettings = builder.Configuration.GetSection(nameof(SRCSettings)).Get<SRCSettings>() ?? 
        throw new Exception("No SRC settings found");

    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.BaseAddress = new Uri(srcSettings.BaseUrl);
    client.Timeout = TimeSpan.FromMinutes(10);

}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new SocketsHttpHandler
    {
        PooledConnectionIdleTimeout = TimeSpan.FromMinutes(10),
    };
}).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
#endregion

#region validators
builder.Services.AddValidatorsFromAssemblyContaining(typeof(ReggressorValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CompanyValidator));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(MeterValidator));
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region app
var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

#region endpoints
app.MapReggressorEndpoints();
app.MapCompanyEndpoints();
app.MapMeterEndpoints();
#endregion

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors("AllowAll");

#region seeder
app.RunMigrator();

app.RunSeeder();
#endregion

if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
    app.Run();
}
else
{
    app.Run("http://0.0.0.0:10000");
}
#endregion