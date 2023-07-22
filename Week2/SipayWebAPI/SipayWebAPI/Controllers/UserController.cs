using Microsoft.AspNetCore.Mvc;
using SipayData.Entities;
using SipayData.Models;
using SipayWebAPI.Services.UserService;

namespace SipayData.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/<UserController>s
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var responseModel = await _userService.GetAllAsync();

        return new JsonResult(responseModel);
    }

    // GET api/<UserController>s/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var responseModel = await _userService.GetByIdAsync(id);

        return new JsonResult(responseModel);
    }

    // POST api/<UserController>s
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User entity)
    {
        await _userService.CreateAsync(entity);

        return new JsonResult(new ResponseModel<string>(entity.FirstName + " " + entity.LastName + " Created"));
    }

    // POST api/<UserController>s
    [HttpPost("login")]
    public async Task<IActionResult> UserLogin(string email, string password)
    {
        var responseModel = await _userService.UserLoginAsync(email,password);

        return new JsonResult(new ResponseModel<string>(responseModel));
    }

    // PUT api/<UserController>s/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User entity)
    {
        entity.Id = id;
        await _userService.UpdateAsync(entity);

        return new JsonResult(new ResponseModel<string>(entity.FirstName + " " + entity.LastName + " Updated"));
    }

    // DELETE api/<UserController>s/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        await _userService.DeleteByIdAsync(id);

        return new JsonResult(new ResponseModel<string>("Deleted"));
    }

    // DELETE api/<UserController>s
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(User entity)
    {
        await _userService.DeleteAsync(entity);

        return new JsonResult(new ResponseModel<string>("Deleted"));
    }

    // PATCH api/<UserController>s/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUser([FromRoute] int id, string password)
    {
        await _userService.UpdateUserPasswordAsync(id, password);

        return new JsonResult(new ResponseModel<string>("Updated"));
    }
}