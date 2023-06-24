namespace ERPApi.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public int QuantityAvailable { get; set; }
    public int VirtualAvailable { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public string? Barcode { get; set; }
    public decimal? Weight { get; set; }
    public string? Type { get; set; }
    public string? Tag { get; set; }

    public Vendor Vendor { get; set; } = null!;
}