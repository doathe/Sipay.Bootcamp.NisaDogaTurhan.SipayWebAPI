using SipayData.Entities;
using SipayWebAPI.Services.BaseService;

namespace SipayWebAPI.Services.CarService;

public interface ICarService : IGenericService<Car>
{
    public Task<IEnumerable<Car>> GetCarsByFilterAsync(string filterValue);
    public Task<IEnumerable<Car>> GetCarsBySortAsync(string sortBy, string sortType);
}
