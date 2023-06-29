using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using BulkyBook.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services (depedencies) to the container
builder.Services.AddControllersWithViews();
// GetConnectionString() only looks for "ConnectionStrings" in appsetting.json
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Services for Identity Razor pages
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();


var app = builder.Build();

// Configure the HTTP request PIPELINE.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();           //1st Middleware to go to, Routing (order in the pipeline matters)
app.UseAuthentication();    // then 2nd Midleware, Authentication/ Authorization
app.UseAuthorization();

app.MapRazorPages();        // mapping routes for Identity Razor pages
app.MapControllerRoute(
    name: "default",        // set the default url: use Home controller, with action Index
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
