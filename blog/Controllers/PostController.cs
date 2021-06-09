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
    public class PostController : ControllerBase
    {
        private PostRepository action = new PostRepository();
        [HttpGet]
        public IActionResult Get()
        {
            var response = action.ReadAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = action.ReadById(id);
            if (response == null)
                return NotFound();
            else
                return Ok(response);
        }

        [HttpGet("title")]
        public IActionResult GetByTitle(string title)
        {
            var response = action.ReadByTitle(title);
            if (response.Count != 0)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("create")]
        public IActionResult Create(Post post)
        {
            UserRepository userRepo = new UserRepository();
            if (post != null && userRepo.ReadById(post.UserId) != null)
            {
                var response = action.Create(post);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPut("update")]
        public IActionResult Update(Post post)
        {
            action.Update(post);
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Post post)
        {
            action.Delete(post);
            return Ok("The post has been deleted successfully");
        }

        [HttpGet("dispose")]
        public void Dispose()
        {
            action.Dispose();
        }
    }
}
