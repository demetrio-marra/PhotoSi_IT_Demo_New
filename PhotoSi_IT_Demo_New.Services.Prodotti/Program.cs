using PhotoSi_IT_Demo_New.Services.Prodotti.Data;
using Microsoft.EntityFrameworkCore;
using PhotoSi_IT_Demo_New.Services.Prodotti.Data.Repositories;
using PhotoSi_IT_Demo_New.Services.Prodotti;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ProdottiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddScoped<ProdottiRepository>();
builder.Services.AddScoped<ProdottiOrdinatiRepository>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new AutomapperProfile()));
builder.Services.AddScoped<ProdottiService>();

var app = builder.Build();

// db init and creation
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<ProdottiDbContext>();
    context.Database.Migrate();
}


// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

// per i test
namespace PhotoSi_IT_Demo_New.Services.Prodotti
{
    public partial class Program { }
}