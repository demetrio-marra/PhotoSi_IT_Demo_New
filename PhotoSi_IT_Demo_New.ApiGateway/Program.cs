using PhotoSi_IT_Demo_New.Infrastructure;
using PhotoSi_IT_Demo_New.Infrastructure.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IPhotoSiServicesBus, BasicHttpPhotoSiServicesBus>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
