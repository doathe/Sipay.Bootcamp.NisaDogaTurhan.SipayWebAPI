using SipayWebAPI.Models.ViewModels.Cars;

namespace SipayWebAPI.Services.CarService;

public interface ICarService
{
    public Task<IEnumerable<CarViewModel>> GetAllAsync();
    public Task<CarViewModel> GetByIdAsync(int id);

    public Task CreateAsync(CreateCarModel entity);
    public Task UpdateAsync(int id, UpdateCarModel entity);
    public Task DeleteByIdAsync(int id);

    //public Task<IEnumerable<Car>> GetCarsByFilterAsync(string filterValue);
    //public Task<IEnumerable<Car>> GetCarsBySortAsync(string sortBy, string sortType);
}
