namespace Infrastructure.Entities;

public class Price : Base
{
    public DateTime InitialDate { get; set; }
    public DateTime FinalDate { get; set; }
    public long InitialTime { get; set; }
    public decimal InitialTimeValue { get; set; }
    public decimal AdditionalHourlyValue { get; set; }
}
