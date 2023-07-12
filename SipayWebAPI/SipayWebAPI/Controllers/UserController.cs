using Microsoft.AspNetCore.Mvc;
using SipayWebAPI.Entities;
using SipayWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SipayWebAPI.Controllers
{
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
            list.Add(new User { Id = 2, Name = "Doğa", Lastname = "Turhan", Email = "doga.tur@gmail.com", Password = "12345aA.", Age = 22 });
            list.Add(new User { Id = 3, Name = "Doğa", Lastname = "Turhan", Email = "doga.tur@gmail.com", Password = "12345aA.", Age = 22 });
        }

        // Action Methods added and Service operations done inside them.

        // GET: api/<UserController>
        [HttpGet]
        public ResponseModel<IEnumerable<User>> GetAll()
        {
            return new ResponseModel<IEnumerable<User>>(list);
        }

        // GET api/<UserController>s/{id}
        [HttpGet("{id}")]
        public ResponseModel<User> Get([FromRoute] int id)
        {
            var responseData = list.FirstOrDefault(i => i.Id == id);
            if (responseData != null)
            {
                return new ResponseModel<User>(responseData);
            }
            else
                throw new Exception("User can not found.");
        }

        // POST api/<UserController>
        [HttpPost]
        public ResponseModel<IEnumerable<User>> Post([FromBody] User user)
        {
            var id = list.Count();
            user.Id = (id + 1);
            list.Add(user);

            return new ResponseModel<IEnumerable<User>>(list);
        }

        // PUT api/<UserController>s/{id}
        [HttpPut("{id}")]
        public ResponseModel<User> Put([FromRoute] int id, [FromBody] User user)
        {
            var updateModel = list.FirstOrDefault(i => i.Id == id);
            if (updateModel != null)
            {
                user.Id = id;
                list.Remove(updateModel);
                list.Add(user);
                return new ResponseModel<User>(user);
            }
            else
                throw new Exception("User can not found.");
        }

        // DELETE api/<UserController>s/{id}
        [HttpDelete("{id}")]
        public ResponseModel<IEnumerable<User>> Delete([FromRoute] int id)
        {
            var responseData = list.FirstOrDefault(i => i.Id == id);
            if (responseData != null)
            {
                list.Remove(responseData);
                return new ResponseModel<IEnumerable<User>>(list);
            }
            else
                throw new Exception("User can not found.");
        }

        // PATCH api/<UserController>s/{id}
        [HttpPatch("{id}")]
        public ResponseModel<User> Patch([FromRoute] int id, string password)
        {
            var responseData = list.FirstOrDefault(i => i.Id == id);
            if (responseData != null)
            {
                list.Remove(responseData);
                responseData.Password = password;
                list.Add(responseData);
                return new ResponseModel<User>(responseData);
            }
            else
                throw new Exception("User can not found.");
        }

        // Update Model ve Patch Model oluşturup onlarada Validation yap
        // Sıralama filtreleme yap.
        // Global Exception Handling'e bak.

    }
}
