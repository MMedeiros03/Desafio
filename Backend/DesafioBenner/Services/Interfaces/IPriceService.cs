using Infrastructure.Entities;

namespace DesafioBenner.Services.Interfaces;

public interface IPriceService
{
    Task<List<Price>> GetAllAsync();

    Task<Price> GetByIdAsync(long id); 

    Task<Price> GetPriceInPeriodAsync(DateTime entryDate, DateTime ?finalDate);

    Task<Price> PostAsync(Price entity);

    Task<Price> PutAsync(Price entity);

    Task<Price> DeleteAsync(long id);
}
