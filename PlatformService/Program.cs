using Microsoft.EntityFrameworkCore;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

// Force Kestrel to listen on port 80
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));

// Register Repository (Interface + Implementation)
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

// Call PrepDb here at startup
PrepDb.PrepPopulation(app, app.Environment.IsProduction());
Console.WriteLine($"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");


app.Run();

