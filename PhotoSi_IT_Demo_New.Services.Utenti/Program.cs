
using Microsoft.EntityFrameworkCore;
using PhotoSi_IT_Demo_New.Services.Utenti;
using PhotoSi_IT_Demo_New.Services.Utenti.Data;
using PhotoSi_IT_Demo_New.Services.Utenti.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UtentiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new AutomapperProfile()));

builder.Services.AddScoped<UtentiRepository>();
builder.Services.AddScoped<UtentiService>();

var app = builder.Build();

// db init and creation
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<UtentiDbContext>();
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
namespace PhotoSi_IT_Demo_New.Services.Utenti
{
    public partial class Program { }
}