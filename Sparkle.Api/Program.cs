using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Sparkle.Api.Data;
using Sparkle.Api.Data.Interfaces;
using Sparkle.Api.Domain.Models;
using Sparkle.Api.Infrastructure;
using Sparkle.Api.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default") ?? 
    throw new Exception("No connection string found");

builder.Services.AddLinqToDBContext<SparkleContext>((provider, options)
    => options.UseSQLite(connectionString).UseDefaultLogging(provider));

builder.Services.AddSingleton<ISeeder, Seeder>();

builder.Services.AddScoped<IRepository<MeterEm, string?>, Reposiotry<MeterEm, string?>>();
builder.Services.AddScoped<IRepository<CompanyEm, string?>, Reposiotry<CompanyEm, string?>>();
builder.Services.AddScoped<IRepository<ReadingsEm, string?>, Reposiotry<ReadingsEm, string? >>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RunMigrator();

app.RunSeeder();

app.Run();
