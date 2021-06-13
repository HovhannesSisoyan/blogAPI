using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.DAL;

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

        [HttpGet]
        public IActionResult Get()
        {
            var response = repository.ReadAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = repository.ReadById(id);
            if (response == null)
                return NotFound();
            else
                return Ok(response);
        }

        [HttpGet("getbytitle")]
        public IActionResult GetByTitle(string title)
        {
            var response = repository.ReadByTitle(title);
            if (response.Count != 0)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
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
