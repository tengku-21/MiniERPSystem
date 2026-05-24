using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Infrastructure.Database;
using Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.API;

public static class InventoryModule
{
    public static IServiceCollection AddInventoryModule(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<InventoryDbContext>(options =>
            options.UseSqlite(config.GetConnectionString("InventoryDb")));

        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IInventoryService, InventoryService>();

        return services;
    }
}