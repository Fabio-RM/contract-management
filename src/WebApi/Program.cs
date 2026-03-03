using System.Reflection;
using Application.Common.Behaviors;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
        configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("Infrastructure")
    )
);

services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

services.AddScoped(
    typeof(IPipelineBehavior<,>),
    typeof(UnitOfWorkBehavior<,>));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();