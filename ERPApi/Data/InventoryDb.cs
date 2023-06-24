using ERPApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERPApi.Data;

class InventoryDb : DbContext
{
    public InventoryDb(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<Product> Products => Set<Product>();
}