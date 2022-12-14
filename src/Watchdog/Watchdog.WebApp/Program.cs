var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHealthChecksUI()
       .AddInMemoryStorage();

var app = builder.Build();

var basePath = Environment.GetEnvironmentVariable("WATCHDOG_API_PATH_BASE");

if (basePath != null)
{
    //k8s ingress 
    app.UsePathBase(basePath);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
                       "default",
                       "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecksUI();

app.Run();