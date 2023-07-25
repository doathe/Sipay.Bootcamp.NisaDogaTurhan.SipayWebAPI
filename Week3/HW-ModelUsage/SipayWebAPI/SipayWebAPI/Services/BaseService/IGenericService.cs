using SipayData.SeedWork;

namespace SipayWebAPI.Services.BaseService;

public interface IGenericService<T> where T : BaseEntity
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T> GetByIdAsync(int id);
    public Task CreateAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(T entity);
    public Task DeleteByIdAsync(int id);
}
