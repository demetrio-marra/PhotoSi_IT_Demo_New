
using Microsoft.EntityFrameworkCore;
using PhotoSi_IT_Demo_New.Services.Recapiti;
using PhotoSi_IT_Demo_New.Services.Recapiti.Data;
using PhotoSi_IT_Demo_New.Services.Recapiti.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<RecapitiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new AutomapperProfile()));
builder.Services.AddScoped<RecapitiRepository>();
builder.Services.AddScoped<RecapitiService>();

var app = builder.Build();

// db init and creation
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<RecapitiDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

// per i test
namespace PhotoSi_IT_Demo_New.Services.Recapiti
{
    public partial class Program { }
}