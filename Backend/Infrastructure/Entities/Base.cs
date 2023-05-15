using Infrastructure.Entities.Interfaces;

namespace Infrastructure.Entities
{
    public class Base : IBase
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
