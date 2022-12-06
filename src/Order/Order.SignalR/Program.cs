using System.Text;
using HealthChecks.UI.Client;
using IntegrationEvents.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Order.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        var jwtSecret = Environment.GetEnvironmentVariable("JwtSecret") ??
                        configuration["Jwt:Secret"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            //Valideaza faptul ca payload-ul din Token a fost semnat cu secretul 
            //disponibil pe server si nu a fost modificat
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidIssuer = Environment.GetEnvironmentVariable("JwtIssuer") ?? configuration["Jwt:Issuer"],
            ValidAudience = Environment.GetEnvironmentVariable("JwtAudience") ?? configuration["Jwt:Audience"],
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

builder.Services.AddSignalR();


builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    var entryAssemblies = typeof(OrderStatusChangedToPaidIntegrationEvent).Assembly;

    x.AddConsumers(entryAssemblies);

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
});

builder.Services.AddHealthChecks();


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

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<OrdersHub>("/ordersHub");

app.Run();