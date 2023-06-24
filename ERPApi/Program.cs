using ERPApi.Data;
using ERPApi.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VendorDb>(opt => opt.UseInMemoryDatabase("VendorList"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/vendoritems", async (VendorDb db) =>
    await db.Vendors.ToListAsync());

app.MapPost("/vendoritems", async (Vendor vendor, VendorDb db) =>
{
    db.Vendors.Add(vendor);
    await db.SaveChangesAsync();
    return Results.Created($"/vendoritems/{vendor.Id}", vendor);
});

app.MapGet("/products", async (string? name, VendorDb db) =>
    await db.Products
        .Where(product => product.Name.Contains(name, StringComparison.OrdinalIgnoreCase) || name == null)
        .ToListAsync());


app.MapPost("/products", async (Product product, VendorDb db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}", product);
});

app.Run();