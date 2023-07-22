using Microsoft.EntityFrameworkCore;
using SipayData.Context;
using SipayData.Entities;
using SipayWebAPI.Services.BaseService;

namespace SipayWebAPI.Services.CarService;

public class CarService : GenericService<Car>, ICarService
{
    private readonly SipayDbContext _context;
    public CarService(SipayDbContext context) : base(context)
    {
        _context = context;
    }

    // Filter by parameter
    public async Task<IEnumerable<Car>> GetCarsByFilterAsync(string filterValue)
    {
        var filteredData = _context.Cars.Where(x => x.Brand.Contains(filterValue) || x.Model.Contains(filterValue)).ToList();
        if (filteredData == null)
            throw new ArgumentNullException(nameof(filterValue));

        return filteredData;
    }

    // Sort by entered data and type
    public async Task<IEnumerable<Car>> GetCarsBySortAsync(string sortBy, string sortType)
    {
        if (sortType == "abc")
        {
            if (sortBy == "brand")
            {
                var sortedData = _context.Cars.OrderBy(x => x.Brand);
                return sortedData;
            }
            else if (sortBy == "model")
            {
                var sortedData = _context.Cars.OrderBy(name => name.Model);
                return sortedData;
            }
        }
        else if (sortType == "cba")
        {
            if (sortBy == "brand")
            {
                var sortedData = _context.Cars.OrderByDescending(name => name.Brand);
                return sortedData;
            }
            else if (sortBy == "model")
            {
                var sortedData = _context.Cars.OrderByDescending(name => name.Model);
                return sortedData;
            }
        }
        throw new ArgumentNullException();
    }
}