using Microsoft.AspNetCore.Mvc;
using SipayData.Entities;
using SipayData.Models;
using SipayWebAPI.Services.CarService;

namespace SipayWebAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    // GET: api/<CarController>s
    [HttpGet]
    public async Task<IActionResult> GetAllCars()
    {
        var responseModel = await _carService.GetAllAsync();

        return new JsonResult(responseModel);
    }

    // GET api/<CarController>s/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCarById([FromQuery] int id)
    {
        var responseModel = await _carService.GetByIdAsync(id);

        return new JsonResult(responseModel);
    }

    // POST api/<CarController>s
    [HttpPost]
    public async Task<IActionResult> CreateCar([FromBody] Car entity)
    {
        await _carService.CreateAsync(entity);

        return new JsonResult(new ResponseModel<string>(entity.LicensePlate + " Created"));
    }

    // PUT api/<CarController>s/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCar([FromQuery] int id, [FromBody] Car entity)
    {
        entity.Id = id;
        await _carService.UpdateAsync(entity);

        return new JsonResult(new ResponseModel<string>(entity.LicensePlate + " Updated"));
    }

    // DELETE api/<CarController>s/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCarById([FromQuery] int id)
    {
        await _carService.DeleteByIdAsync(id);

        return new JsonResult(new ResponseModel<string>("Deleted"));
    }

    // DELETE api/<CarController>s
    [HttpDelete]
    public async Task<IActionResult> DeleteCar(Car entity)
    {
        await _carService.DeleteAsync(entity);

        return new JsonResult(new ResponseModel<string>("Deleted"));
    }

    // GET api/<CarController>s/filter?filterValue=a
    [HttpGet("filter")]
    public async Task<IActionResult> GetCarsByFilter([FromQuery] string filterValue)
    {
        var responseModel = await _carService.GetCarsByFilterAsync(filterValue);

        return new JsonResult(new ResponseModel<IEnumerable<Car>>(responseModel));
    }

    // GET api/<CarController>s/sort?sortBy=name&sortType=abc
    [HttpGet("sort")]
    public async Task<IActionResult> GetCarsBySort([FromQuery] string sortBy, [FromQuery] string sortType)
    {
        var responseModel = await _carService.GetCarsBySortAsync(sortBy, sortType);

        return new JsonResult(new ResponseModel<IEnumerable<Car>>(responseModel));
    }
}