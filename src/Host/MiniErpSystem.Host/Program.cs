using Inventory.API;
using Products.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(ProductsModule).Assembly)
    .AddApplicationPart(typeof(InventoryModule).Assembly);

builder.Services.AddProductsModule(builder.Configuration);
builder.Services.AddInventoryModule(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();