using System.Data.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;
using Testcontainers.PostgreSql;

namespace Tests.IntegrationTests;

public class DatabaseFixture : IAsyncLifetime
{
    public ServiceProvider ServiceProvider { get; private set; }

    private PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .Build();
    
    private DbConnection _connection;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        
        ServiceProvider = TestServiceProvider.Build(_dbContainer.GetConnectionString());
        
        using var scope = ServiceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        await db.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        using var scope = ServiceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Isso apaga os dados de todas as tabelas mapeadas no EF
        await db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"clients\" RESTART IDENTITY CASCADE;");
    }
}