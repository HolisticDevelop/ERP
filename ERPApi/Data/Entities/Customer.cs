using System.ComponentModel;

namespace ERPApi.Entities;



public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string IdentificationNumber { get; set; }
    public IdentificationType IdentificationType { get; set; }
    // Additional properties specific to Colombia
    public string TaxId { get; set; }
    public string Nationality { get; set; }
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    
    
}

public enum IdentificationType
{
    [Description("Tarjeta de Identidad")]
    TarjetaIdentidad,

    [Description("Cédula de Ciudadanía")]
    CedulaCiudadania,

    [Description("Certificado Registraduría sin Identificación")]
    CertificadoRegistraduria,

    [Description("Tarjeta de Extranjería")]
    TarjetaExtranjeria,

    [Description("Cédula de Extranjería")]
    CedulaExtranjeria,

    [Description("Pasaporte")]
    Pasaporte,

    [Description("Documento de Identificación Extranjero")]
    DocumentoExtranjero,

    [Description("Sin Identificación del Exterior o para Uso Definido DIAN")]
    SinIdentificacionExterior,

    [Description("Documento de Identificación Extranjeros Persona Jurídica")]
    DocumentoExtranjeroJuridica,

    [Description("Carné Diplomático")]
    CarneDiplomatico,

    [Description("Permiso Especial de Permanencia PEP")]
    PermisoPermanenciaPEP,

    [Description("Permiso por Protección Temporal PPT")]
    PermisoProteccionTemporalPPT
}



public enum Gender
{
    Male,
    Female,
    Other
}

