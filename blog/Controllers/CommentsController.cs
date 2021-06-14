using blog.DAL;
using blog.DAL.Repository;
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
    public class CommentsController : ControllerBase
    {
        private CommentRepository repository;
        public CommentsController(CommentRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("postId")]
        public IActionResult ReadAll(int postId)
        {
            return Ok(repository.ReadAll(postId));
        }

        [HttpGet("CommentId")]
        public IActionResult ReadById(int commentId)
        {
            return Ok(repository.ReadById(commentId));
        }

        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            return Ok(repository.Create(comment));
        }

        [HttpPut]
        public IActionResult Update(Comment comment)
        {
            return Ok(repository.Update(comment));
        }

        [HttpDelete]
        public IActionResult Delete(Comment comment)
        {
            repository.Delete(comment);
            return Ok();
        }
    }
}
