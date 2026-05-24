using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Interfaces;
using Products.Application.Services;
using Products.Infrastructure.Database;
using Products.Infrastructure.Repositories;

namespace Products.API;

public static class ProductsModule
{
    public static IServiceCollection AddProductsModule(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<ProductsDbContext>(options =>
            options.UseSqlite(config.GetConnectionString("ProductsDb")));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}