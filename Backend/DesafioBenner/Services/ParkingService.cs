using DesafioBenner.DTO;
using DesafioBenner.Repositories.Interfaces;
using DesafioBenner.Services.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace DesafioBenner.Services;

public class ParkingService : IParkingService
{
    private readonly IParkingRepository _repository;
    private readonly IPriceService _priceService;
    private Utils _utils;

    public ParkingService(IParkingRepository repository, IPriceService priceService, Utils utils)
    {
        _repository = repository;
        _priceService = priceService;
        _utils = utils;
    }

    public async Task<List<Parking>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Parking> GetByIdAsync(long id)
    {
        return await _repository.GetByIdAsync(id);
    }

    private async Task<Parking> GetByLicensePlate(string licensePlate)
    {
        return await _repository.GetDbSet().FirstOrDefaultAsync(vehicle => vehicle.LicensePlate == licensePlate && vehicle.DeleteDate == null);
    }

    public async Task<Parking> PostAsync(Parking entity)
    {
        bool currentPrice = await _priceService.GetPriceIsValid(entity.EntryDate);
        if (currentPrice == false) throw new Exception("Este valor não é mais aplicável para essa data de entrada.");

        return await _repository.PostAsync(entity);
    }

    public async Task<Parking> PutAsync(ParkingDepartureDTO entity)
    {
        bool currentPrice = await _priceService.GetPriceIsValid(entity.DepartureDate);

        if (currentPrice == false) throw new Exception("Este valor não é mais aplicável para essa data de saida.");

        var currentVehicle = await GetByLicensePlate(entity.LicensePlate);

        if (currentVehicle == null) throw new Exception("Veiculo não foi encontrado");

        var lenghOfStay = _utils.CalculateLenghtOfStay(currentVehicle.EntryDate, entity.DepartureDate);

        Price price = await _priceService.GetByIdAsync(1);

        decimal amoutToPay = _utils.CalculateAmountToPay(lenghOfStay, price);

        currentVehicle.AmountCharged = amoutToPay;
        currentVehicle.DepartureDate = entity.DepartureDate;

        return await _repository.PutAsync(currentVehicle);
    }
    public async Task<Parking> DeleteAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }
}
