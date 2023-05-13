using ArMapAdmin.Models;
using ArMapAdmin.Services;
using MongoDB.Bson;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ModelsDatabaseSettings>(
    builder.Configuration.GetSection("ModelsDatabase"));
builder.Services.AddSingleton<ModelsService>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

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
    pattern: "{controller=Home}/{action=Show}/{id?}");

app.Run();
