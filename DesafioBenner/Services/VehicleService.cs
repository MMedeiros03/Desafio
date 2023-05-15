using DesafioBenner.Repositories.Interfaces;
using DesafioBenner.Services.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace DesafioBenner.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _repository;
    private readonly IPriceRepository _priceRepository;
    private Utils _utils;

    public VehicleService(IVehicleRepository repository, IPriceRepository priceRepository, Utils utils)
    {
        _repository = repository;
        _priceRepository = priceRepository;
        _utils = utils;
    }

    public async Task<List<Vehicle>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Vehicle> GetByIdAsync(string licensePlate)
    {
        return await _repository.GetDbSet().FirstOrDefaultAsync(vehicle => vehicle.LicensePlate == licensePlate && vehicle.DeleteDate == null);
    }

    public async Task<Vehicle> PostAsync(Vehicle entity)
    {
        return await _repository.PostAsync(entity);
    }

    public async Task<Vehicle> PutAsync(Vehicle entity)
    {
        var lenghOfStay = _utils.CalculateLenghtOfStay(entity.EntryDate, entity.DepartureDate);

        Price price = await _priceRepository.GetByIdAsync(1);

        decimal amoutToPay = _utils.CalculateAmountToPay(lenghOfStay, price);

        return await _repository.PutAsync(entity);
    }
    public async Task<Vehicle> DeleteAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }
}
