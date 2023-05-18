using Infrastructure.Entities;

namespace Infrastructure.Utils;
public class Utils
{
    public double CalculateLenghtOfStay(DateTime entryDate, DateTime? DepartureDate)
    {
        TimeSpan LenghtOfStay = (TimeSpan)(entryDate - DepartureDate);
        return Math.Abs(Math.Round(LenghtOfStay.TotalMinutes, 2));
    }

    public decimal CalculateAmountToPay(double lengthOfStay, Price price)
    {
        if (lengthOfStay <= 30)
        {
            return price.InitialTimeValue / 2;
        }
        else
        {
            decimal valueToPay = (decimal)lengthOfStay * price.InitialTimeValue / (decimal)price.InitialTime;
            double toleranceInMinutes = Math.Abs(price.InitialTime - lengthOfStay);
            if (toleranceInMinutes > 10)
            {
                return Math.Round(valueToPay + price.AdditionalHourlyValue,2);
            }
            else
            {
                return Math.Round(valueToPay,2);
            }
        }
    }

    public decimal ValidateLenghOfStay(TimeSpan time)
    {
        if(time.Hours == 0 && time.Minutes >= 1)
        {
            return 30;
        }
        else
        {
            return time.Hours;
        }
    }

}
