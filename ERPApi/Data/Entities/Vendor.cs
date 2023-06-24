namespace ERPApi.Entities;

public class Vendor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? NIT { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }

    public ICollection<Product> Products { get; } = new List<Product>();
} 