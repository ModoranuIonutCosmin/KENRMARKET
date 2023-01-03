using Customers.Application.Consumers;
using Customers.Application.Features;
using Customers.Application.Interfaces;
using Customers.Application.Workers;
using Customers.Infrastructure.Data_Access;
using Customers.Infrastructure.Data_Access.v1;
using HealthChecks.UI.Client;
using IntegrationEvents.Contracts;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder         = WebApplication.CreateBuilder(args);
var isInDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";


var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddHostedService<CancelExpiringReservationsWorker>();

builder.Host.UseSerilog((context, config) =>
{
    var elasticSearchSettings = new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearch:Uri"]))
                                {
                                    TypeName             = null,
                                    AutoRegisterTemplate = true,
                                    // ModifyConnectionSettings = (c) => c.BasicAuthentication(context.Configuration["ElasticSearch:User"],
                                    //     context.Configuration["ElasticSearch:Password"]),
                                    IndexFormat =
                                        $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",

                                    NumberOfShards   = 2,
                                    NumberOfReplicas = 1
                                };

    config.Enrich.FromLogContext()
          .Enrich.WithMachineName()
          .WriteTo.Console()
          .WriteTo.Elasticsearch(elasticSearchSettings)
          .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
          .ReadFrom.Configuration(context.Configuration);
});

//MediatR
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<CustomersDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
                      opt => { opt.MigrationsAssembly(typeof(CustomersDBContext).Assembly.FullName); });
});



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IReservationsRepository, ReservationsRepository>();


builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<CustomersDBContext>(outbox =>
    {
        outbox.UseSqlServer();
        outbox.UseBusOutbox();
    });

    x.SetKebabCaseEndpointNameFormatter();

    var entryAssemblies = typeof(ICustomersRepository).Assembly;

    x.AddConsumers(entryAssemblies);

    if (isInDevelopment)
    {
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(configuration["EventQueue:Host"], "/", h =>
            {
                h.Username(configuration["EventQueue:Username"]);
                h.Password(configuration["EventQueue:Password"]);
            });
            Configure(cfg, context);

            cfg.AutoStart = true;
        });
    }
    else
    {
        x.UsingAzureServiceBus((context, cfg) =>
        {
            cfg.Host(Environment.GetEnvironmentVariable("SERVICE-BUS-CONNECTIONSTRING"));

            Configure(cfg, context);

            cfg.AutoStart = true;
        });
    }
});

void Configure(IBusFactoryConfigurator busFactoryConfigurator,
    IBusRegistrationContext busRegistrationContext)
{
    busFactoryConfigurator.ReceiveEndpoint("reservation-made-customers",
                                           e => e.ConfigureConsumer<
                                                   ReservationMadeForItemsEventHandler>(busRegistrationContext));


    busFactoryConfigurator.ReceiveEndpoint("order-payed-customers",
                                           e => e
                                               .ConfigureConsumer<
                                                   OrderPaymentSuccessfulIntegrationEventHandler>(busRegistrationContext));
    
    busFactoryConfigurator.ReceiveEndpoint("new-customer-created-customers",
                                           e => e.ConfigureConsumer<NewCustomerRegisteredIntegrationEventHandler>(busRegistrationContext));


}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());






builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddTransient<IReservationsService, ReservationsService>();


builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion                   = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions                   = true;
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

builder.Services.AddHealthChecks()
       .AddSqlServer(configuration.GetConnectionString("SqlServer"),
                     failureStatus: HealthStatus.Degraded);


var app = builder.Build();




app.MapHealthChecks("/hc", new HealthCheckOptions
                           {
                               Predicate      = _ => true,
                               ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                           });
app.MapHealthChecks("/liveness", new HealthCheckOptions
                                 {
                                     Predicate = r => r.Name.Contains("self")
                                 });

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();