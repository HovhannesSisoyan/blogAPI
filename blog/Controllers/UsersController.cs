using blog.DAL;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserRepository repository;

        public UsersController(UserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (user.Password != user.FirstName || user.Password != user.FirstName)
            {
                var resposne = repository.Create(user);
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
            var response = repository.ReadAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult ReadById(int id)
        {
            var response = repository.ReadById(id);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public IActionResult Update(User user)
        {
            if (user.Password != user.FirstName || user.Password != user.FirstName)
            {
                var response = repository.Update(user);
                return Ok(response);
            }
            else
            {
                return BadRequest("The password must not be the same as the first or last name.");
            }
        }

        [HttpDelete]
        public IActionResult Delete(User user)
        {
            repository.Delete(user);
            return Ok("The user has been deleted successfully");
        }
    }
}
