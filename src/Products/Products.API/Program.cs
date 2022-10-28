using Microsoft.AspNetCore.Mvc;
using Products.Application.Features;
using Products.Application.Interfaces;
using Products.Application.Interfaces.Services;
using Products.Infrastructure.Data_Access;
using Products.Infrastructure.Data_Access.Config;
using Products.Infrastructure.Data_Access.v1;
using Products.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DbContext>();
builder.Services.AddSingleton<IMongoDBSettings, MongoDBSettings>(
    _ => builder.Configuration
        .GetSection("ConnectionStrings:Mongo")
        .Get<MongoDBSettings>());

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