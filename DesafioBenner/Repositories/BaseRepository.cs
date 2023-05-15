using Infrastructure.DataBase;
using Infrastructure.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioBenner.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IBase
{
    public readonly Context _context;
    public BaseRepository(Context context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().Where(x => x.DeleteDate == null).ToListAsync();
    }

    public async Task<T> GetByIdAsync(long id)
    {
        dynamic result = await _context.Set<T>().FindAsync(id);

        if (result == null || result.DeleteDate != null)
        {
            throw new KeyNotFoundException("Registro não encontrado!");
        }

        return result;
    }

    public async Task<T> PostAsync(T entity)
    {
        entity.CreateDate = DateTime.Now;

        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> PutAsync(T entity)
    {
        T existingEntity = (T)_context.Find(entity.GetType(), entity.Id);
        if (existingEntity == null)
        {
            throw new KeyNotFoundException("Registro não encontrado!");
        }
        existingEntity.UpdateDate = DateTime.Now;
        //_context.Entry(existingEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<T> DeleteAsync(long id)
    {
        dynamic result = await _context.Set<T>().FindAsync(id);

        if (result == null || result.DeleteDate != null)
        {
            throw new KeyNotFoundException("Registro não encontrado!");
        }

        result.DeleteDate = DateTime.Now;

        _context.Set<T>().Update(result);
        await _context.SaveChangesAsync();
        return result;
    }

    public DbSet<T> GetDbSet()
    {
        return _context.Set<T>();
    }
}
