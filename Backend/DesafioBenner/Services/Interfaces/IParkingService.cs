using DesafioBenner.DTO;
using Infrastructure.Entities;
namespace DesafioBenner.Services.Interfaces;

public interface IParkingService
{
    Task<List<Parking>> GetAllAsync();

    Task<Parking> GetByIdAsync(long id);

    Task<Parking> PostAsync(Parking entity);

    Task<Parking> PutAsync(ParkingDepartureDTO entity);

    Task<Parking> DeleteAsync(long id);
}
