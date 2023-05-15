using System.ComponentModel.DataAnnotations;
namespace Infrastructure.Entities;

public class Vehicle : Base
{
    [Required]
    public DateTime EntryDate { get; set; }
    [Required]
    public string LicensePlate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
}
