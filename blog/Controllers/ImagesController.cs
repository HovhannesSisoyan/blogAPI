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
    public class ImagesController : ControllerBase
    {
        private ImageRepository repository;

        public ImagesController(ImageRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        public IActionResult Create(Image image)
        {
            var response = repository.Create(image);
            return Ok(response);
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
        public IActionResult Update(Image image)
        {
            var response = repository.Update(image);
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult Delete(Image image)
        {
            repository.Delete(image);
            return Ok("The user has been deleted successfully");
        }
    }
}
