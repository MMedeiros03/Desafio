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

    /// <summary>
    /// Obtém todos os registros do estacionamento.
    /// </summary>
    public async Task<List<Parking>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    /// <summary>
    /// Obtém um registro do estacionamento pelo ID.
    /// </summary>
    public async Task<Parking> GetByIdAsync(long id)
    {
        return await _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Obtém um registro do estacionamento pela placa do veiculo
    /// </summary>
    private async Task<Parking> GetByLicensePlate(string licensePlate)
    {
        return await _repository.GetDbSet().FirstOrDefaultAsync(vehicle => vehicle.LicensePlate == licensePlate && vehicle.DeleteDate == null);
    }

    /// <summary>
    /// Busca se existe um veiculo estacionado no momento pelo numero da placa
    /// </summary>
    private async Task<Parking> GetByLicensePlateActive(string licensePlate)
    {
        return await _repository.GetDbSet().FirstOrDefaultAsync(vehicle => vehicle.LicensePlate == licensePlate && vehicle.DepartureDate == null && vehicle.DeleteDate == null);
    }

    /// <summary>
    /// Busca se existe um veiculo estacionado no momento pelo numero da placa
    /// </summary>
    public async Task<Parking> PostAsync(Parking entity)
    {
        dynamic currentPrice = await _priceService.GetPriceInPeriodAsync(entity.EntryDate,null);

        if (currentPrice == null) throw new KeyNotFoundException("Não existe tabela de preço vigente para essa data de entreda.");

        var currentVehicle = await GetByLicensePlateActive(entity.LicensePlate);

        if (currentVehicle != null) throw new BadHttpRequestException("Ja existe um veiculo cadastrado com a placa informada.");

        return await _repository.PostAsync(entity);
    }

    /// <summary>
    /// Atualiza o veiculo estacionado marcando o horario de saída
    /// e faz o calculo do valor a ser pago
    /// </summary>
    public async Task<Parking> PutAsync(ParkingDepartureDTO entity)
    {
        var currentVehicle = await GetByLicensePlate(entity.LicensePlate);

        if (currentVehicle == null) throw new KeyNotFoundException("Veiculo não foi encontrado");

        dynamic currentPrice = await _priceService.GetPriceInPeriodAsync(currentVehicle.EntryDate, entity.DepartureDate);

        string validateDays = "";

        if (currentPrice == null) throw new KeyNotFoundException("Este não existe tabela de preço vigente aplicável para essa data de saida.");


        var lenghOfStay = _utils.CalculateLenghtOfStay(currentVehicle.EntryDate, entity.DepartureDate);

        decimal amountToPay = _utils.CalculateAmountToPay(lenghOfStay, currentPrice);

        TimeSpan time = TimeSpan.FromMinutes(lenghOfStay);

        var chargedTime = _utils.ValidateLenghOfStay(time);

        currentVehicle.AmountCharged = amountToPay;
        currentVehicle.DepartureDate = entity.DepartureDate;
        currentVehicle.LenghOfStay = $"{chargedTime}:{time.Minutes}:{time.Seconds}";
        currentVehicle.ChargedTime = chargedTime;
        currentVehicle.PriceCharged = currentPrice.InitialTimeValue;

        return await _repository.PutAsync(currentVehicle);
    }

    /// <summary>
    /// Exclui um estacionamento pelo ID.
    /// </summary>
    public async Task<Parking> DeleteAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }
}
