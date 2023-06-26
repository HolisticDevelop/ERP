// using ERPApi.Data;
// using ERPApi.Entities;
// using Microsoft.AspNetCore.Http.HttpResults;
// using Microsoft.EntityFrameworkCore;
//
// var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<VendorDb>(opt => opt.UseInMemoryDatabase("VendorList"));
// builder.Services.AddEndpointsApiExplorer();
// var app = builder.Build();
//
//
//
// app.MapGet("/", () => "Hello World!");
//
// app.MapGet("/vendoritems", async (VendorDb db) =>
//     await db.Vendors.ToListAsync());
//
// app.MapPost("/vendoritems", async (Vendor vendor, VendorDb db) =>
// {
//     db.Vendors.Add(vendor);
//     await db.SaveChangesAsync();
//     return Results.Created($"/vendoritems/{vendor.Id}", vendor);
// });
//
// app.MapGet("/products", async (string? name, VendorDb db) =>
//     await db.Products
//         .Where(product => product.Name.Contains(name, StringComparison.OrdinalIgnoreCase) || name == null)
//         .ToListAsync());
//
//
// app.MapPost("/products", async (Product product, VendorDb db) =>
// {
//     db.Products.Add(product);
//     await db.SaveChangesAsync();
//     return Results.Created($"/products/{product.Id}", product);
// });
//
// // Customers
// app.MapGet("/customers", async (VendorDb db) => 
//     await db.Customers.ToListAsync());
//
// app.MapPost("/customers", async (Customer customer, VendorDb db) =>
// {
//     db.Customers.Add(customer);
//     await db.SaveChangesAsync();
//     return Results.Created($"/customers/{customer.Id}", customer);
// });
//
// app.Run();
using static System.Environment;
using static System.Reflection.Assembly;
namespace ERP;

public static class Program
{
    static Program() =>
        CurrentDirectory = Path.GetDirectoryName(GetEntryAssembly().Location);

    public static void Main(string[] args)
    {
        var configuration = BuildConfiguration(args);

        ConfigureWebHost(configuration).Build().Run();
    }

    private static IConfiguration BuildConfiguration(string[] args)
        => new ConfigurationBuilder()
            .SetBasePath(CurrentDirectory)
            .Build();

    private static IWebHostBuilder ConfigureWebHost(
        IConfiguration configuration)
        => new WebHostBuilder()
            .UseStartup<Startup>()
            .UseConfiguration(configuration)
            .UseContentRoot(CurrentDirectory)
            .UseKestrel();
}