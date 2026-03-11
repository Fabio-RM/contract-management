using Application;
using Application.Clients.Interfaces.Repositories;
using Application.Common.Behaviors;
using Application.Common.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Conventions;

namespace WebApi.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
        
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        
        services.AddScoped<IClientWriteRepository, ClientWriteRepository>();
        services.AddScoped<IClientQueryRepository, ClientQueryRepository>();
        
        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(
                typeof(InfrastructureAssemblyMarker).Assembly,
                typeof(ApplicationAssemblyMarker).Assembly,
                typeof(WebApiAssemblyMarker).Assembly)
        );

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        
        return services;
    }

    public static IServiceCollection AddConventions(this IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            opt.Conventions.Add(new RoutePrefixConvention(new RouteAttribute("api/v1")));
        });

        return services;
    }
}