using DesafioBenner.Repositories.Interfaces;
using DesafioBenner.Services.Interfaces;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioBenner.Services
{
    public class PriceService : IPriceService
    {
        public readonly IPriceRepository _repository;

        public PriceService(IPriceRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Busca todos os registros da tabela Price
        /// </summary>
        public async Task<List<Price>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Busca um preço na tabela baseado na datas informadas.
        /// </summary>
        public async Task<Price> GetPriceInPeriodAsync(DateTime initialDate, DateTime ?finalDate)
        {
            return  await _repository.GetDbSet().FirstOrDefaultAsync(pr => pr.InitialDate <= initialDate && pr.FinalDate >= (finalDate ?? initialDate) && pr.DeleteDate == null);
        }

        /// <summary>
        /// Busca um registro da tabela Price baseando-se pelo Id
        /// </summary>
        public async Task<Price> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Cria um novo registro na tabela de Price
        /// </summary>
        public async Task<Price> PostAsync(Price entity)
        {
            dynamic existPrice = await GetPriceInPeriodAsync(entity.InitialDate, entity.FinalDate);
            if (existPrice != null) throw new BadHttpRequestException("Ja existe uma tabela de preço vigente nesse periodo"); 
            return await _repository.PostAsync(entity);
        }


        /// <summary>
        /// Atualiza um registro na tabela de Price
        /// </summary>
        public async Task<Price> PutAsync(Price entity)
        {
            return await _repository.PutAsync(entity);
        }

        /// <summary>
        /// Exclui um registro na tabela de Price
        /// </summary>
        public async Task<Price> DeleteAsync(long id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
