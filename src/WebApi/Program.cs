using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("Infrastructure")
                )
        );

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();