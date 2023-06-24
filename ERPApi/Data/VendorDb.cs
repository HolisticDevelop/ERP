using ERPApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERPApi.Data;

class VendorDb : DbContext
{
    public VendorDb(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<Product> Products => Set<Product>();

}