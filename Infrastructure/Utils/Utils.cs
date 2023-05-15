using Infrastructure.Entities;

namespace Infrastructure.Utils;
public class Utils
{
    public double CalculateLenghtOfStay(DateTime entryDate, DateTime? DepartureDate)
    {
        TimeSpan LenghtOfStay = (TimeSpan)(entryDate - DepartureDate);
        return Math.Abs(Math.Round(LenghtOfStay.TotalMinutes, 2));
    }

    public decimal CalculateAmountToPay(double lenghtOfStay, Price price)
    {
        decimal amount = 0;

        if (lenghtOfStay <= 30)
        {
            return amount = price.InitialTimeValue / 2;
        }
        else
        {
            double tolerance = Math.Ceiling(lenghtOfStay - price.InitialTime);
            if (tolerance > 10)
            {
                return amount = price.InitialTimeValue + price.AdditionalHourlyValue;
            }
            else
            {
                return price.InitialTimeValue;
            }
        }
    }

}
