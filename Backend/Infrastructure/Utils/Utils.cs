using Infrastructure.Entities;

namespace Infrastructure.Utils;
public class Utils
{
    /// <summary>
    /// Calcula o tempo de permanencia do veiculo no estacionamento em minutos.
    /// </summary>
    /// <param name="entryDate">Data de entrada.</param>
    /// <param name="departureDate">Data de saída.</param>
    /// <returns>A duração da estadia em minutos.</returns>
    public double CalculateLenghtOfStay(DateTime entryDate, DateTime? departureDate)
    {
        TimeSpan LenghtOfStay = (TimeSpan)(departureDate - entryDate);
        return Math.Abs(Math.Round(LenghtOfStay.TotalMinutes, 2));
    }

    /// <summary>
    /// Calcula o valor a ser pago com base na permanencia do veiculo no 
    /// estacionamento e no preço da hora do estacionamento.
    /// </summary>
    /// <param name="lengthOfStay">Tempo de permanencia em minutos.</param>
    /// <param name="price">Entidade Price vigente.</param>
    /// <returns>O valor a ser pago.</returns>
    public decimal CalculateAmountToPay(double lengthOfStay, Price price)
    {
        if (lengthOfStay <= 30)
        {
            return price.InitialTimeValue / 2;
        }
        else
        {
            decimal valueToPay = (decimal)lengthOfStay * price.InitialTimeValue / (decimal)price.InitialTime;

            TimeSpan time = TimeSpan.FromMinutes(lengthOfStay);

            double toleranceInMinutes = Math.Abs(price.InitialTime - lengthOfStay);

            if (time.Minutes > 10)
            {
                return Math.Round(valueToPay + price.AdditionalHourlyValue, 2);
            }
            else
            {
                return Math.Round(valueToPay, 2);
            }
        }
    }

    /// <summary>
    /// Valida a duração da estadia e retorna o tempo cobrado em horas.
    /// </summary>
    /// <param name="time">Tempo de permanencia.</param>
    /// <returns>O tempo cobrado em horas.</returns>
    public decimal ValidateHoursLengthOfStay(TimeSpan time)
    {
        if (time.Days == 0)
        {
            return time.Hours;
        }
        else
        {
            var hoursByDays = time.Days * 24;
            return time.Hours + hoursByDays;
        }

    }

}
