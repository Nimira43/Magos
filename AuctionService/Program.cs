using AuctionService.Data;
using AuctionService.Errors;
using Mapster;
using Microsoft.EntityFrameworkCore;

TypeAdapterConfig.GlobalSettings.Scan(typeof(Program).Assembly);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(options =>
{
    options.UseNpgsql((builder.Configuration.GetConnectionString("DefaultConnection")));
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
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