using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using Products.Application.Features;
using Products.Application.Interfaces;
using Products.Application.Interfaces.Services;
using Products.Infrastructure.Data_Access;
using Products.Infrastructure.Data_Access.Config;
using Products.Infrastructure.Data_Access.v1;
using Products.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var isInDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var settings = builder.Configuration
    .GetSection("ConnectionStrings:Mongo")
    .Get<MongoDBSettings>();

builder.Services.AddSingleton<ProductsDbContext>();
builder.Services.AddSingleton<IMongoDBSettings, MongoDBSettings>(
    _ => settings);

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(settings.Host));
builder.Services.AddSingleton<IMongoDatabase>(provider => provider.GetService<IMongoClient>()
    .GetDatabase(settings.DatabaseName));


builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    var entryAssemblies = typeof(IProductsService).Assembly;

    x.AddConsumers(entryAssemblies);

    var isInDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

    if (isInDevelopment)
    {
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(configuration["EventQueue:Host"], "/", h =>
            {
                h.Username(configuration["EventQueue:Username"]);
                h.Password(configuration["EventQueue:Password"]);
            });


            cfg.ConfigureEndpoints(context);

            cfg.AutoStart = true;
        });
    }
    else
    {
        x.UsingAzureServiceBus((context, cfg) =>
        {
            cfg.Host(Environment.GetEnvironmentVariable("SERVICE-BUS-CONNECTIONSTRING"));
            
            cfg.ConfigureEndpoints(context);

            cfg.AutoStart = true;
        });
    }


});

builder.Services.AddTransient<IProductsRepository, ProductsRepository>();
builder.Services.AddTransient<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IProductsService, ProductsService>();


builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
});

// var rabbitConString =
//     $"amqp://{configuration["EventQueue:Username"]}:{configuration["EventQueue:Password"]}@{configuration["EventQueue:Host"]}:5672";

builder.Services.AddHealthChecks()
    .AddMongoDb(configuration["ConnectionStrings:Mongo:Host"], "MongoDb", HealthStatus.Degraded);


var app = builder.Build();


app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecks("/liveness", new HealthCheckOptions
{
    Predicate = r => r.Name.Contains("self")
});
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();


app.Run();