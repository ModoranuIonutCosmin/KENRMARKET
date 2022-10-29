using Cart.Application.Features;
using Cart.Application.Interfaces;
using Cart.Application.Interfaces.Services;
using Cart.Infrastructure.Data_Access;
using Cart.Infrastructure.Data_Access.v1;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<CartsDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
        opt => { opt.MigrationsAssembly(typeof(CartsDBContext).Assembly.FullName); });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<CartsDBContext>(outbox =>
    {
        outbox.UseSqlServer();
        outbox.UseBusOutbox();
    });

    x.SetKebabCaseEndpointNameFormatter();

    var entryAssemblies = typeof(ICartRepository).Assembly;

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

builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();


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