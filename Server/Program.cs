using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddMemoryCache(); // Copilot: Added this line to register the service

var app = builder.Build();

app.UseCors("AllowAll");

// Copilot suggested this code for validation of the JSON structure. Using the classes from the Shared project.
app.MapGet("/api/v1/productlist", ([FromServices] IMemoryCache cache) => {
    if (!cache.TryGetValue("products", out Product[] products))
    {
        products = new[] {
            new Product{ 
                Id = 1, 
                Name = "Laptop", 
                Price = 1200.50, 
                Stock = 25,
                Category = new Category { Id = 101, Name = "Electronics" }
            },
            new Product { 
                Id = 2, 
                Name = "Headphones", 
                Price = 50.00, 
                Stock = 100,
                Category = new Category { Id = 102, Name = "Accessories" } }
        };

        cache.Set("products", products, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        });
    }

    return Results.Ok(products);
});

app.Run();
