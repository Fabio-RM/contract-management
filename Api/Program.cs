var builder = WebApplication.CreateBuilder(args);

// Add MediatR to the service container
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
