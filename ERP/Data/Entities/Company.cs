namespace ERPApi.Entities;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } // Nombre
    public string TaxId { get; set; } // NIT
    public string Address { get; set; } // Direccion
    public string City { get; set; } // Ciudad
    public string PostalCode { get; set; } // CodigoPostal
    public string Country { get; set; } // Pais
    public string Email { get; set; }
    public string PhoneNumber { get; set; } // NumeroTelefono
    public string? Website { get; set; } // SitioWeb
    public string? Logo { get; set; } // Logo path or URL
    // Additional properties specific to a company in Colombia
    public string? CommercialRegistryId { get; set; } // RegistroMercantil
    public string? ChamberOfCommerceId { get; set; } // CamaraComercio
    public string? LegalRepresentative { get; set; } // RepresentanteLegal

    public ICollection<Employee> Employees { get; } = new List<Employee>();

}

