using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Newspaper.Application.Extensions;
using NewsPaper.DataAccess.Data;
using NewsPaper.DataAccess.Extensions;
using Newspaper.Domain.Entities;
using Newspaper.Infrastructure.Extensions;
using Newspaper.Web.Infrastructure.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();  
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
await DataSeeder.InitializeAdminsAsync(services);

app.Run();