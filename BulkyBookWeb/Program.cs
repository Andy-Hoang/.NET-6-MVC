using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services (depedencies) to the container
// GetConnectionString() only looks for "ConnectionStrings" in appsetting.json
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

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

app.UseAuthorization();     // then 2nd Midleware, Authorization

app.MapControllerRoute(
    name: "default",        // set the deault url: use Home controller, with action Index
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
