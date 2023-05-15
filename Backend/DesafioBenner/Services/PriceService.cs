using DesafioBenner.Repositories.Interfaces;
using DesafioBenner.Services.Interfaces;
using Infrastructure.Entities;

namespace DesafioBenner.Services
{
    public class PriceService : IPriceService
    {
        public readonly IPriceRepository _repository; 

        public PriceService(IPriceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Price>> GetAllAsync()
        {
           return await _repository.GetAllAsync();
        }

        public async Task<Price> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Price> PostAsync(Price entity)
        {
            return await _repository.PostAsync(entity);
        }

        public async Task<Price> PutAsync(Price entity)
        {
            return await _repository.PutAsync(entity);
        }
        public async Task<Price> DeleteAsync(long id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
