using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Application.Interfaces;
using Order.Infrastructure.Data_Access;
using Order.Infrastructure.Data_Access.v1;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<OrdersDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
        opt => { opt.MigrationsAssembly(typeof(OrdersDBContext).Assembly.FullName); });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Services

builder.Services.AddTransient<IOrderRepository, OrderRepository>();

//Mediatr

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

//MassTransit (rabbitmq??)

builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<OrdersDBContext>(outbox =>
    {
        outbox.UseSqlServer();
        outbox.UseBusOutbox();
    });

    x.SetKebabCaseEndpointNameFormatter();

    var entryAssemblies = typeof(IOrderRepository).Assembly;

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