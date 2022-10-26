using System.Reflection;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payments.API.Config;
using Payments.Application.Interfaces;
using Payments.Application.Payments;
using Payments.Infrastructure.Data_Access;
using Payments.Infrastructure.Data_Access.v1;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<PaymentsDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), 
        opt =>
    {
        opt.MigrationsAssembly(typeof(PaymentsDBContext).Assembly.FullName);
    });
});


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IPaymentsRepository, PaymentsRepository>();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

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




//Stripe

var stripeSettings = builder.Configuration.GetSection("Stripe").Get<StripeSettings>();

StripeConfiguration.ApiKey = stripeSettings.SecretKey;

builder.Services.Configure<StripeSettings>(
    builder.Configuration.GetSection("Stripe"));

builder.Services.Configure<FrontEndInfo>(
    builder.Configuration.GetSection("Client"));


//Event bus (rabitmq??)

builder.Services.AddMassTransit(x =>
{
    x.AddEntityFrameworkOutbox<PaymentsDBContext>(outbox =>
    {
        outbox.UseSqlServer();

        outbox.UseBusOutbox();
    });

    x.SetKebabCaseEndpointNameFormatter();

    var entryAssemblies = Assembly.GetAssembly(nameof(Payments.Application).GetType());


    x.AddConsumers(entryAssemblies);

    x.UsingRabbitMq((context, cfg) =>

    {
        cfg.Host(configuration["EventQueue:Host"], "/", h =>
        {
            h.Username(configuration["EventQueue:Username"]);
            h.Password(configuration["EventQueue:Password"]);
        });
        cfg.AutoStart = true;

        cfg.ConfigureEndpoints(context);
    });


});

//Services

builder.Services.AddTransient<IPaymentsGatewayService, PaymentsGatewayService>();

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
