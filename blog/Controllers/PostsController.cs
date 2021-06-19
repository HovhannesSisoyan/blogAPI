using blog.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace blog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private PostRepository repository;
        public PostsController(PostRepository repository)
        {
            this.repository = repository;
        }
        [EnableCors("CorsPolicy")]
        [HttpGet("/posts")]
        public IActionResult Get()
        {
            var response = repository.ReadAll();
            return Ok(response);
        }

        [EnableCors("CorsPolicy")]
        [HttpGet("/posts/{id?}")]
        public IActionResult GetById(int id)
        {
            System.Console.WriteLine(id);
            var response = repository.ReadById(id);
            if (response == null)
                return NotFound();
            else
                return Ok(response);
        }

        [EnableCors("CorsPolicy")]
        [HttpGet("/posts/search")]
        public IActionResult GetByTitle([FromQuery] string key)
        {
            var response = repository.ReadByTitle(key);
            return Ok(response);
        }
        [EnableCors("CorsPolicy")]
        [HttpPost("/posts")]
        [Authorize]
        public IActionResult Create(Post post)
        {
            UserRepository userRepo = new UserRepository();
            if (post != null && userRepo.ReadById(post.UserId) != null)
            {
                var response = repository.Create(post);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPut]
        public IActionResult Update(Post post)
        {
            repository.Update(post);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Post post)
        {
            repository.Delete(post);
            return Ok("The post has been deleted successfully");
        }
    }
}
