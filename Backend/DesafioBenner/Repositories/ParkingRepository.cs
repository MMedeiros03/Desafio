using DesafioBenner.Repositories.Interfaces;
using Infrastructure.DataBase;
using Infrastructure.Entities;

namespace DesafioBenner.Repositories
{
    public class ParkingRepository : BaseRepository<Parking>, IParkingRepository
    {
        public ParkingRepository(Context context) : base(context)
        {
        }
    }
}
