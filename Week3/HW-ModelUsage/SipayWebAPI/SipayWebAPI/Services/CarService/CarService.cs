using Microsoft.EntityFrameworkCore;
using SipayData.Context;
using SipayData.Entities;
using SipayWebAPI.Models.ViewModels.Cars;

namespace SipayWebAPI.Services.CarService;

public class CarService : ICarService
{
    private readonly SipayDbContext _context;
    public CarService(SipayDbContext context)
    {
        _context = context;
    }

    // GetAll method returns all data from the requested table.
    public async Task<IEnumerable<CarViewModel>> GetAllAsync()
    {
        List<Car> cars = await _context.Set<Car>().Include(i => i.User).ToListAsync();
        List<CarViewModel> viewModels = new List<CarViewModel>();
        foreach (var item in cars)
        {
            viewModels.Add(new CarViewModel()
            {
                Brand = item.Brand,
                Model = item.Model,
                LicensePlate = item.LicensePlate,
                Year = item.Year,
                FuelType = item.FuelType,
                UserName = item.User.FirstName + " " + item.User.LastName
            });
        }

        return viewModels;
    }

    // GetById method returns data from the requested table with id data.
    public async Task<CarViewModel> GetByIdAsync(int id)
    {
        var car = await _context.Set<Car>().Include(i => i.User).FirstOrDefaultAsync(i => i.Id == id);
        CarViewModel viewModel = new CarViewModel
        {
            Brand = car.Brand,
            Model = car.Model,
            LicensePlate = car.LicensePlate,
            Year = car.Year,
            FuelType = car.FuelType,
            UserName = car.User.FirstName + " " + car.User.LastName
        };

        return viewModel;
    }

    // Create method saves the entered data in the correct table.
    public async Task CreateAsync(CreateCarModel entity)
    {
        Car car = new Car
        {
            Brand = entity.Brand,
            Model = entity.Model,
            LicensePlate = entity.LicensePlate,
            Year = entity.Year,
            FuelType = entity.FuelType,
            UserId = entity.UserId
        };
        car.CreatedOn = DateTime.UtcNow;
        car.UpdatedOn = DateTime.UtcNow;

        var existCheck = _context.Set<Car>().Any(i => i.LicensePlate == car.LicensePlate);
        if (existCheck)
        {
            throw new InvalidOperationException("User already exist");
        }

        await _context.Set<Car>().AddAsync(car);
        await _context.SaveChangesAsync();
    }

    // Delete method, deletes the entered data from the correct table with id data.
    public async Task DeleteByIdAsync(int id)
    {
        var entity = await _context.Set<Car>().FirstOrDefaultAsync(i => i.Id == id);
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _context.Set<Car>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    // Update method, updates the entered data from the correct table.
    public async Task UpdateAsync(int id, UpdateCarModel entity)
    {
        var existingEntity = _context.Set<Car>().Include(i => i.User).FirstOrDefault(i => i.Id == id);
        if (existingEntity == null)
            throw new ArgumentNullException(nameof(entity));

        Car car = new Car
        {
            Id = id,
            Brand = entity.Brand,
            Model = entity.Model,
            LicensePlate = entity.LicensePlate,
            Year = entity.Year,
            FuelType = entity.FuelType
        };
        car.UpdatedOn = DateTime.UtcNow;
        car.UserId = existingEntity.UserId;
        car.User = existingEntity.User;

        _context.Entry(existingEntity).CurrentValues.SetValues(car);
        await _context.SaveChangesAsync();
    }

    //// Filter by parameter
    //public async Task<IEnumerable<Car>> GetCarsByFilterAsync(string filterValue)
    //{
    //    var filteredData = _context.Cars.Where(x => x.Brand.Contains(filterValue) || x.Model.Contains(filterValue)).ToList();
    //    if (filteredData == null)
    //        throw new ArgumentNullException(nameof(filterValue));

    //    return filteredData;
    //}

    //// Sort by entered data and type
    //public async Task<IEnumerable<Car>> GetCarsBySortAsync(string sortBy, string sortType)
    //{
    //    if (sortType == "abc")
    //    {
    //        if (sortBy == "brand")
    //        {
    //            var sortedData = _context.Cars.OrderBy(x => x.Brand);
    //            return sortedData;
    //        }
    //        else if (sortBy == "model")
    //        {
    //            var sortedData = _context.Cars.OrderBy(name => name.Model);
    //            return sortedData;
    //        }
    //    }
    //    else if (sortType == "cba")
    //    {
    //        if (sortBy == "brand")
    //        {
    //            var sortedData = _context.Cars.OrderByDescending(name => name.Brand);
    //            return sortedData;
    //        }
    //        else if (sortBy == "model")
    //        {
    //            var sortedData = _context.Cars.OrderByDescending(name => name.Model);
    //            return sortedData;
    //        }
    //    }
    //    throw new ArgumentNullException();
    //}
}