namespace ERPApi.Entities;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string TaxId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string Industry { get; set; }
    public bool IsActive { get; set; }
}