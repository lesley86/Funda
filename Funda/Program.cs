using Application;
using Application.Queries;
using Core.Repository;
using Funda.ErrorHandling;
using Infrastructure.ExternalApi;
using NLog.Extensions.Logging;
using Infrastructure.Persistence.Sql;
using Infrastructure.Persistence;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(loggingBuilder =>
{
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

builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddDbContext<ApiContext>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMemoryCache();
builder.Services.AddMediatR(cfg =>
{
	cfg.RegisterServicesFromAssemblies(typeof(GetHousesQuery).Assembly, typeof(GetHousesQueryHandler).Assembly);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }