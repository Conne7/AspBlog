using Blog.Application.DTO;
using Blog.Application.UseCases.Queries.Comments;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // GET: api/<CommentController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/<CommentController>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] BlogContext _context)
        {
            var comment = _context.Comments.Include(x => x.Replies).Include(x=>x.Author).Include(x => x.Likes).Where(x => x.Id == id).FirstOrDefault();

            if (comment == null) return NotFound();
            else return Ok(new
            {
                Author = comment.Author.Username,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                RepliesCount = comment.Replies.Count(),
                Replies = comment.Replies.Select(x=> new
                {
                    Author = x.Author.Username,
                    Reply = x.Text
                }).ToList()
            });

        }

        // POST api/<CommentController>
        [HttpPost]
        [Authorize]
        public IActionResult Post(
            [FromBody] CommentAPostDto dto,
            [FromServices] UseCaseHandler _handler,
            [FromServices] ICommentOnAPost command
            )
        {


            _handler.HandleCommand(command,dto);

            return Created();
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
