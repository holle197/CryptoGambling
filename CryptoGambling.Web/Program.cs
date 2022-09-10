using CryptoGambling.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CryptoGambling.Web.Areas.Identity.Data;
using CryptoGambling.Data.DataContext;
using CryptoGambling.Data.DataManaging;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Connection") ?? throw new InvalidOperationException("Connection string 'Connection' not found.");

builder.Services.AddDbContext<UserIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<UserIdentity>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<UserIdentityDbContext>();

builder.Services.AddDbContext<DataUserContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(40);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddOptions();
builder.Services.AddScoped<IDataManager, DataManager>();

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
app.UseAuthentication(); ;
app.UseAuthorization();
app.MapRazorPages();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
