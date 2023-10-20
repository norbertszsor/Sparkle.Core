using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Sparkle.Domain.Data;
using Sparkle.Shared.Extensions;
using Sparkle.Infrastructure;
using Sparkle.Shared.Helpers;
using Sparkle.Handling;
using Sparkle.Api.Extensions;
using SparkleRegressor.Client;

#region builder

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(options =>
{
    options.AddConsole();
    options.AddDebug();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(policyBuilder => policyBuilder.Expire(TimeSpan.FromSeconds(20)));
    options.AddPolicy("UntilNextHour",
        policyBuilder => policyBuilder.Expire(TimeSpan.FromMinutes(60 - DateTime.UtcNow.Minute)));
});

var connectionString = builder.Configuration.GetConnectionString("Default") ??
                       throw ThrowHelper.Throw<WebApplication>("no connection string found");

builder.Services.AddLinqToDBContext<SparkleContext>((provider, options) =>
    options.UseSQLite(connectionString).UseDefaultLogging(provider));

builder.Services.AddInfrastructure();

builder.Services.AddHandling();

builder.Services.AddSparkleRegressorClient(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

#endregion

#region app

var app = builder.Build();

app.UseCors("AllowAll");

app.UseOutputCache();

app.AddEndpoints();

app.UseSwagger();

app.UseSwaggerUI();

app.RunMigrator();

app.RunSeeder();

if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
    app.Run();
}
else
{
    app.Run();
}

#endregion
