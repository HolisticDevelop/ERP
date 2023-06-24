using System.ComponentModel.DataAnnotations;

namespace ERPApi.Entities;

public class Address
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Street1 is a required field.")]
    public string Street1 { get; set; }
    public string City { get; set; }
}