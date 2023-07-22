using Microsoft.AspNetCore.Mvc;
using SipayData.Entities;
using SipayData.Models;
using SipayWebAPI.Services.RentalService;

// Rental Services didn't finished yet, I will rewrite them later.

namespace SipayWebAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class RentalController : ControllerBase
{
    private readonly IRentalService _rentalService;

    public RentalController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    // GET: api/<RentalController>s
    [HttpGet]
    public async Task<IActionResult> GetAllRentals()
    {
        var responseModel = await _rentalService.GetAllAsync();

        return new JsonResult(responseModel);
    }

    // GET api/<RentalController>s/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRentalById(int id)
    {
        var responseModel = await _rentalService.GetByIdAsync(id);

        return new JsonResult(responseModel);
    }

    // POST api/<RentalController>s
    [HttpPost]
    public async Task<IActionResult> CreateRental([FromBody] Rental entity)
    {
        await _rentalService.CreateAsync(entity);

        return new JsonResult(new ResponseModel<string>("Created"));
    }

    // PUT api/<RentalController>s/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRental(int id, [FromBody] Rental entity)
    {
        entity.Id = id;
        await _rentalService.UpdateAsync(entity);

        return new JsonResult(new ResponseModel<string>("Updated"));
    }

    // DELETE api/<RentalController>s/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRentalById(int id)
    {
        await _rentalService.DeleteByIdAsync(id);

        return new JsonResult(new ResponseModel<string>("Deleted"));
    }

    // DELETE api/<RentalController>s
    [HttpDelete]
    public async Task<IActionResult> DeleteRental(Rental entity)
    {
        await _rentalService.DeleteAsync(entity);

        return new JsonResult(new ResponseModel<string>("Deleted"));
    }
}
