using blog.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepository action = new UserRepository();
        [HttpPost("create")]
        public IActionResult Create(User user)
        {
            if (user.Password != user.FirstName || user.Password != user.FirstName)
            {
                var resposne = action.Create(user);
                if (resposne != null)
                {
                    return Ok(resposne);
                }
                else
                {
                    return Conflict(); 
                }
            }
            else
            {
                return BadRequest("The password must not be the same as the first or last name.");
            }
        }

        [HttpGet]
        public IActionResult ReadAll()
        {
            var response = action.ReadAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult ReadById(int id)
        {
            var response = action.ReadById(id);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("update")]
        public IActionResult Update(User user)
        {
            if (user.Password != user.FirstName || user.Password != user.FirstName)
            {
                var response = action.Update(user);
                return Ok(response);
            }
            else
            {
                return BadRequest("The password must not be the same as the first or last name.");
            }
        }

        [HttpGet("dispose")]
        public void Dispose()
        {
            action.Dispose();
        }

        [HttpDelete("delete")]
        public IActionResult Delete(User user)
        {
            action.Delete(user);
            return Ok("The user has been deleted successfully");
        }
    }
}
