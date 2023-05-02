using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FashionCatalogue.Data;
using FashionCatalogue;
using Microsoft.AspNetCore.Identity;
using FashionCatalogue.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FashionCatalogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FashionCatalogContextConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddDefaultIdentity<FashionCatalogUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<FashionCatalogContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}"
//    );
app.MapRazorPages();

app.Run();
