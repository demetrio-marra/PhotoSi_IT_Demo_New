using PhotoSi_IT_Demo_New.Services.Ordini.Data;
using Microsoft.EntityFrameworkCore;
using PhotoSi_IT_Demo_New.Services.Ordini.Data.Repositories;
using PhotoSi_IT_Demo_New.Services.Ordini;
using PhotoSi_IT_Demo_New.Infrastructure.Abstractions;
using PhotoSi_IT_Demo_New.Infrastructure;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<OrdiniDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddScoped<OrdiniRepository>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new AutomapperProfile()));
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IPhotoSiServicesBus, BasicHttpPhotoSiServicesBus>();
builder.Services.AddScoped<OrdiniService>();

var app = builder.Build();

// db init and creation
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<OrdiniDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

// per i test
namespace PhotoSi_IT_Demo_New.Services.Ordini
{
    public partial class Program { }
}