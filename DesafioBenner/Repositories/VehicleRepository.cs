using DesafioBenner.Repositories.Interfaces;
using Infrastructure.DataBase;
using Infrastructure.Entities;

namespace DesafioBenner.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(Context context) : base(context)
        {
        }
    }
}
