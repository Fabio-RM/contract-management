using Application;
using Application.Clients.Interfaces.Repositories;
using Application.Common.Behaviors;
using Application.Common.Interfaces;
using Core;
using Core.Interfaces.Repositories;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Clients;
using Infrastructure.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tests.IntegrationTests;

public static class TestServiceProvider
{
    public static ServiceProvider Build(string connectionString)
    {
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(
            opt => opt.UseNpgsql(connectionString));

        services.AddLogging(b => b.AddConsole());
        
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(
                typeof(TestsAssemblyMarker).Assembly,
                typeof(DomainAssemblyMarker).Assembly,
                typeof(ApplicationAssemblyMarker).Assembly,
                typeof(InfrastructureAssemblyMarker).Assembly)
            );
        
        services.AddTransient(typeof(IDateTimeProvider), typeof(DateTimeProvider));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

        services.AddScoped<IClientWriteRepository, ClientWriteRepository>();
        services.AddScoped<IClientQueryRepository, ClientQueryRepository>();
        
        return services.BuildServiceProvider();
    }
}