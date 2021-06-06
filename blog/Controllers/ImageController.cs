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
    public class ImageController : ControllerBase
    {
        ImageRepository action = new ImageRepository();
        [HttpPost("create")]
        public IActionResult Create(Image image)
        {
            var response = action.Create(image);
            return Ok(response);
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
        public IActionResult Update(Image image)
        {
            var response = action.Update(image);
            return Ok(response);
        }

        [HttpGet("dispose")]
        public void Dispose()
        {
            action.Dispose();
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Image image)
        {
            action.Delete(image);
            return Ok("The user has been deleted successfully");
        }
    }
}
