using Infrastructure.Entities;
namespace DesafioBenner.Services.Interfaces;

public interface IVehicleService
{
    Task<List<Vehicle>> GetAllAsync();

    Task<Vehicle> GetByIdAsync(string licensePlate);

    Task<Vehicle> PostAsync(Vehicle entity);

    Task<Vehicle> PutAsync(Vehicle entity);

    Task<Vehicle> DeleteAsync(long id);
}
