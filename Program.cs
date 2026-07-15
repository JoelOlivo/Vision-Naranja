using Microsoft.Extensions.FileProviders;
using VisionNaranja.Data;
using VisionNaranja.Data.Repositories;
using VisionNaranja.Services;
using VisionNaranja.Services.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Data
builder.Services.AddSingleton<DbConnectionFactory>();

//Repositories
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductMediaRepository>();
builder.Services.AddScoped<EntrepreneurRepository>();
builder.Services.AddScoped<EntrepreneurshipRepository>();

//Services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<HomeService>();
builder.Services.AddScoped<FileStorageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        builder.Configuration["Storage:RootPath"]!),

    RequestPath = "/media"
});

app.Run();
