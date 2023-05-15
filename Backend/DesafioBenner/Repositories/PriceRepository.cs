using DesafioBenner.Repositories.Interfaces;
using Infrastructure.DataBase;
using Infrastructure.Entities;

namespace DesafioBenner.Repositories
{
    public class PriceRepository : BaseRepository<Price>, IPriceRepository
    {
        public PriceRepository(Context context) : base(context){ }
    }
}
