using AuctionService.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;

TypeAdapterConfig.GlobalSettings.Scan(typeof(Program).Assembly);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(options =>
{
    options.UseNpgsql((builder.Configuration.GetConnectionString("DefaultConnection")));
});

var app = builder.Build();

app.MapControllers();

try
{
    DbInitialiser.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine($"Error initialising database: {e.Message}" );
}

app.Run();