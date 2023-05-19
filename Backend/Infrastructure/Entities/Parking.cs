using System.ComponentModel.DataAnnotations;
namespace Infrastructure.Entities;

public class Parking : Base
{
    [Required]
    public DateTime EntryDate { get; set; }
    [Required]
    public string LicensePlate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public string? LenghOfStay { get; set; }
    public decimal? PriceCharged { get; set; }
    public decimal? ChargedTime { get; set; }
    public decimal? AmountCharged { get; set; }

}
