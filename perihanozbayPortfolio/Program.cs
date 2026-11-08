
using perihanozbayPortfolio.Context;

var builder = WebApplication.CreateBuilder(args);

// MVC servisleri
builder.Services.AddControllersWithViews();

// Entity Framework Core - SQL Server
builder.Services.AddDbContext<PortfolioContext>();

var app = builder.Build();

// HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// ADMIN AREA ROUTING
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}")
    .WithStaticAssets();

// DEFAULT MVC ROUTING
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
