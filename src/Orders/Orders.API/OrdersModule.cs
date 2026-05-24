using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Interfaces;
using Orders.Application.Services;
using Orders.Infrastructure.HttpClients;
using Orders.Infrastructure.Database;
using Orders.Infrastructure.Repositories;

namespace Orders.API;

public static class OrdersModule
{
    public static IServiceCollection AddOrdersModule(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<OrdersDbContext>(options =>
            options.UseSqlite(config.GetConnectionString("OrdersDb")));

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();

        // HTTP clients point back to the same host (monolith)
        services.AddHttpClient<IProductClient, ProductHttpClient>(client =>
        {
            client.BaseAddress = new Uri(config["Services:BaseUrl"]!);
        });

        services.AddHttpClient<IInventoryClient, InventoryHttpClient>(client =>
        {
            client.BaseAddress = new Uri(config["Services:BaseUrl"]!);
        });

        return services;
    }
}