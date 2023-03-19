using Application;
using AutoMapper;
using Funda;
using Infrastructure.ExternalApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

var config = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
		.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
		.Build();

// Add services to the container.
builder.Configuration.AddConfiguration(config);
builder.Services.Configure<ExternalApiBaseUrlOptions>(builder.Configuration.GetSection("ExternalApiBaseUrl"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(loggingBuilder =>
{
    // configure Logging with NLog
    loggingBuilder.ClearProviders();
    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
    loggingBuilder.AddNLog(config);
});

builder.Services.AddScoped<IFundaService, FundaService>();
builder.Services.AddScoped<IFundaKeyService, FundaKeyService>();
builder.Services.AddScoped<IFundaExternalApi, FundaExternalApi>();
builder.Services.AddScoped<IFundaRelativeUrlBuilder, FundaRelativeUrlBuilder>();
builder.Services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();
builder.Services.AddScoped<IPollyAsyncRetryPolicy, PollyAsyncRetryPolicy>();

builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }