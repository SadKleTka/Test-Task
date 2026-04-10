using Microsoft.EntityFrameworkCore;
using BitcoinApi.Context;
using BitcoinApi.ExceptionHandler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<WorkerService, WorkerServiceImpl>();
builder.Services.AddHttpClient<BitcoinGettingService>();
builder.Services.AddHostedService<BitcoinGettingService>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseMiddleware<ExceptionHandler>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();