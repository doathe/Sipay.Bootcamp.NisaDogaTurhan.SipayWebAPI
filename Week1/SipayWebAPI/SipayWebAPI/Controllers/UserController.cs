using Microsoft.AspNetCore.Mvc;
using SipayWebAPI.Entities;
using SipayWebAPI.Models;

namespace SipayWebAPI.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class UserController : ControllerBase
{
    // Defined list property for user data
    private List<User> list;

    // Dummy Data added in Constructor
    public UserController()
    {
        list = new List<User>();

        list.Add(new User { Id = 1, Name = "Doğa", Lastname = "Turhan", Email = "doga.tur@gmail.com", Password = "12345aA.", Age = 22 });
        list.Add(new User { Id = 2, Name = "Nisa", Lastname = "Turhan", Email = "nisa@gmail.com", Password = "12345aA.", Age = 20 });
        list.Add(new User { Id = 3, Name = "Kardem", Lastname = "Mese", Email = "karr@gmail.com", Password = "12345aA.", Age = 23 });
    }

    // Action Methods added and Service operations done inside them.

    // GET: api/<UserController>
    [HttpGet]
    public ResponseModel<IEnumerable<User>> GetAll()
    {
        return new ResponseModel<IEnumerable<User>>(list);                          // Returns list that I created on constructor.
    }

    // GET api/<UserController>s/{id}
    [HttpGet("{id}")]
    public IActionResult Get([FromRoute] int id)
    {
        var responseData = list.FirstOrDefault(i => i.Id == id);                    // Finds object that have entered id from route.
        if (responseData != null)
        {
            return Ok(new ResponseModel<User>(responseData));                       // If the Data is not null returns Data that found.
        }
        else
            return NotFound();                                                      // If the Data is null returns 404 Not Found.
    }

    // POST api/<UserController>
    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        var id = list.Count();
        user.Id = (id + 1);                                                         // Finds the count of the list and increments it by one, adding it to the id.
        list.Add(user);                                                             // Entered User data is added to the list.

        return Created("User created",new ResponseModel<IEnumerable<User>>(list));  // With 201 Created, the list is returned.
    }

    // PUT api/<UserController>s/{id}
    [HttpPut("{id}")]
    public IActionResult Put([FromRoute] int id, [FromBody] User user)
    {
        var updateModel = list.FirstOrDefault(i => i.Id == id);                     // Finds object that have entered id from route.
        if (updateModel != null)
        {
            user.Id = id;                                                           // If data is not null, it syncs the id of the entered user value to the id given in the Route.
            list.Remove(updateModel);                                               // It deletes the value found with the id entered in the route from the list.
            list.Add(user);                                                         // It adds the entered user value to the list, so that the requested data is updated.
            return Ok(new ResponseModel<User>(user));                               // Returns the updated data.
        }
        else
            return NotFound();                                                      // If the Data is null returns 404 Not Found.
    }

    // DELETE api/<UserController>s/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var responseData = list.FirstOrDefault(i => i.Id == id);                    // Finds object that have entered id from route.
        if (responseData != null)
        {
            list.Remove(responseData);                                              // If data is not null, It deletes the value found with the id entered in the route from the list.
            return Ok(new ResponseModel<IEnumerable<User>>(list));                  // Returns list.
        }
        else
            return NotFound();                                                      // If the Data is null returns 404 Not Found.
    }

    // PATCH api/<UserController>s/{id}
    [HttpPatch("{id}")]
    public IActionResult Patch([FromRoute] int id, string password)
    {
        var responseData = list.FirstOrDefault(i => i.Id == id);                    // Finds object that have entered id from route.
        if (responseData != null)
        {
            list.Remove(responseData);                                              // Deletes the value found with the id entered in the route from the list.
            responseData.Password = password;                                       // Sets the password of the found Data to the value to be changed.
            list.Add(responseData);                                                 // It adds the entered user value to the list, so that the requested data is updated.
            return Ok(new ResponseModel<User>(responseData));                       // Returns the updated data.
        }
        else
            return NotFound();                                                      // If the Data is null returns 404 Not Found.
    }

    // GET api/<UserController>s/filter?filterValue=a
    [HttpGet("filter")]
    public IActionResult GetByFilter([FromQuery] string filterValue)
    {
        var filteredData = list.Where(user => user.Name.Contains(filterValue) || user.Lastname.Contains(filterValue)).ToList(); //Checks if there is a value with the entered filter value.
        if (filteredData != null)
        {
            return Ok(new ResponseModel<IEnumerable<User>>(filteredData));          // If Data is not null, it returns filtered Data.
        }
        else
            return NotFound();                                                      // If the Data is null returns 404 Not Found.
    }

    // GET api/<UserController>s/sort?sortBy=name&sortType=abc
    [HttpGet("sort")]
    public IActionResult GetBySort([FromQuery] string sortBy, [FromQuery] string sortType)  // abc = ascending, cba = descending
    {
        if (sortType == "abc")
        {
            if (sortBy == "name")
            {
                var sortedData = list.OrderBy(name => name.Name);                   // Sorts ascending by name.
                return Ok(sortedData);                                              // Returns sorted Data.
            }
            else if (sortBy == "lastname")
            {
                var sortedData = list.OrderBy(name => name.Lastname);               // Sorts ascending by lastname.
                return Ok(sortedData);                                              // Returns sorted Data.
            }
        }
        else if (sortType == "cba")
        {
            if (sortBy == "name")
            {
                var sortedData = list.OrderByDescending(name => name.Name);         // Sorts descending by name.
                return Ok(sortedData);                                              // Returns sorted Data.
            }
            else if (sortBy == "lastname")
            {
                var sortedData = list.OrderByDescending(name => name.Lastname);     // Sorts descending by lastname.
                return Ok(sortedData);                                              // Returns sorted Data.
            }
        }

        return NotFound();                                                          // Returns 404 Not Found.
    }
}
