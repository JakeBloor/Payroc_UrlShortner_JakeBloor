using Payroc_UrlShortner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<URLDBContext>(
    options => options.UseInMemoryDatabase(databaseName: "URLS"));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapFallback(RedirectURL);

app.Run();

static async Task RedirectURL(HttpContext httpContext)
{
    var db = httpContext.RequestServices.GetRequiredService<URLDBContext>();
    var collection = db.Urls.ToList();

    var path = httpContext.Request.Path.ToUriComponent().Trim('/');
    var id = BitConverter.ToInt32(WebEncoders.Base64UrlDecode(path));
    var entry = collection.Find(p => p.Id == id);

    httpContext.Response.Redirect(entry?.URL ?? "/");

    await Task.CompletedTask;
}