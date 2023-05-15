namespace Infrastructure.Entities.Interfaces;

public interface IBase
{
    public long Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }

}
