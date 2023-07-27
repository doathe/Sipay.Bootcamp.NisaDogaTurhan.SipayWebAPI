using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SipayData.Context;
using SipayData.SeedWork;

namespace SipayWebAPI.Services.BaseService;

public class GenericService<T> : IGenericService<T> where T : BaseEntity
{
    // Database context class defined for data
    private readonly SipayDbContext _context;
    public GenericService(SipayDbContext context)
    {
        _context = context;
    }
    // Create method saves the entered data in the correct table.
    public async Task CreateAsync(T entity)
    {
        entity.CreatedOn = DateTime.UtcNow;
        entity.UpdatedOn = DateTime.UtcNow;
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    // Delete method, deletes the entered data from the correct table.
    public async Task DeleteAsync(T entity)
    {
        var existingEntity = await _context.Set<T>().FindAsync(entity);
        if(existingEntity == null)
            throw new ArgumentNullException(nameof(entity));

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    // Delete method, deletes the entered data from the correct table with id data.
    public async Task DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    // GetAll method returns all data from the requested table.
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    // GetById method returns data from the requested table with id data.
    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    // Update method, updates the entered data from the correct table.
    public async Task UpdateAsync(T entity)
    {
        var existingEntity = _context.Set<T>().Find(entity);
        if (existingEntity == null)
            throw new ArgumentNullException(nameof(entity));

        entity.UpdatedOn = DateTime.UtcNow;
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }
}
