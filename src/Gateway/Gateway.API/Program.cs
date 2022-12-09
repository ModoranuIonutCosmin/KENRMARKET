using System.Security.Authentication;
using System.Text;
using Gateway.API.Auth;
using Gateway.API.Exceptions;
using Gateway.API.Interfaces;
using Gateway.API.Services;
using Gateway.Application.Profiles;
using Gateway.Domain.Entities;
using Gateway.Domain.Exceptions;
using Gateway.Infrastructure.Data_Access;
using HealthChecks.UI.Client;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging.Console;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polly;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
    .AddHttpClient("ProductsService",
        config => { config.BaseAddress = new Uri(builder.Configuration["Services:Products"]); })
    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

builder.Services
    .AddHttpClient("PaymentsService",
        config => { config.BaseAddress = new Uri(builder.Configuration["Services:Payments"]); })
    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));


builder.Services
    .AddHttpClient("CartService", config => { config.BaseAddress = new Uri(builder.Configuration["Services:Cart"]); })
    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

builder.Services
    .AddHttpClient("OrdersService",
        config => { config.BaseAddress = new Uri(builder.Configuration["Services:Orders"]); })
    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

builder.Services
    .AddHttpClient("CustomersService",
        config => { config.BaseAddress = new Uri(builder.Configuration["Services:Customers"]); })
    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));


builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

///EF
builder.Services.AddDbContext<AuthenticationDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
        opt => { opt.MigrationsAssembly(typeof(AuthenticationDbContext).Assembly.FullName); });
});
/// 

///Identity

builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AuthenticationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 3000;

    options.SignIn.RequireConfirmedEmail = false;
});


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


builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "KENRMARKET", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

///

// ProblemDetails

builder.Services.AddProblemDetails((options) =>
{

    options.IncludeExceptionDetails = (_, _) => true;
    // This will map NotImplementedException to the 501 Not Implemented status code.
    options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

    // This will map HttpRequestException to the 503 Service Unavailable status code.
    options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);


    options.MapToStatusCode<AuthenticationException>(StatusCodes.Status403Forbidden);


    options.MapToStatusCode<InvalidConfirmationLinkException>(StatusCodes.Status400BadRequest);
    options.MapToStatusCode<InvalidPasswordResetLink>(StatusCodes.Status400BadRequest);


    options.MapToStatusCode<ProductDoesntExistException>(StatusCodes.Status404NotFound);
    options.MapToStatusCode<UserNotFoundException>(StatusCodes.Status404NotFound);

    options.MapToStatusCode<StockForOrderNotValidatedException>(StatusCodes.Status403Forbidden);

    // Because exceptions are handled polymorphically, this will act as a "catch all" mapping, which is why it's added last.
    // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
    options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
});

//Automapper

//TODO: Better way?
builder.Services.AddAutoMapper(typeof(UpdateCartRequestToCartDetailsProfile).Assembly);

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

builder.Services.AddTransient<IUserPasswordResetService, UserPasswordResetService>();
builder.Services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IPaymentsService, PaymentsService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<ICartAggregatesService, CartAggregatesService>();
builder.Services.AddScoped<IOrdersAggregatesService, OrdersAggregatesService>();

builder.Services.AddHealthChecks()
    .AddSqlServer(configuration.GetConnectionString("SqlServer"), failureStatus: HealthStatus.Degraded);


var app = builder.Build();

var basePath = Environment.GetEnvironmentVariable("GATEWAY_API_PATH_BASE");

if (basePath != null)
{
    //k8s ingress 
    app.UsePathBase(basePath);
}




// app.Use(async (context, next) =>
// {
//     using var loggerFactory = LoggerFactory.Create(builder =>
// {
//     builder.AddSimpleConsole(i => i.ColorBehavior = LoggerColorBehavior.Enabled);
// });

//     var logger = loggerFactory.CreateLogger<Program>();


//     if (context.Request.Headers.TryGetValue("X-Forwarded-PathBase", out var pathBase))
//     {
//         context.Request.PathBase = pathBase.Last();

//         if (context.Request.Path.StartsWithSegments(context.Request.PathBase, out var path))
//         {
//             context.Request.Path = path;
//         }
//     }

//     await next();
// });

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

app.UseProblemDetails();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();