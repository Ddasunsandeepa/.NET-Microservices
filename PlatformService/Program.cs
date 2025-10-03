using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.AsyncDataServices;
using PlatformService.SyncDataServices.Http;
using PlatformService.SyncDataServices.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Force Kestrel to listen on port 80 (container friendly)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

// Environment
var env = builder.Environment;

// Configure DbContext
if (env.IsProduction())
{
    Console.WriteLine("--> Using SqlServer Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}
else
{
    Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseInMemoryDatabase("InMem"));
}

// Register repository
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

// Register external services
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddGrpc();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add controllers
builder.Services.AddControllers();

// OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine($"--> CommandService Endpoint {builder.Configuration["CommandService"]}");

var app = builder.Build();

// Middleware & Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // optional

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<GrpcPlatformService>();

// Expose proto file
app.MapGet("/protos/platforms.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
});

// Seed database
PrepDb.PrepPopulation(app, app.Environment.IsProduction());

Console.WriteLine($"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

app.Run();
