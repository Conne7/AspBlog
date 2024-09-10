using Blog.Application.DTO;
using Blog.Application.UseCases.Commands.Posts;
using Blog.Application.UseCases.Queries.Posts;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        // GET: api/<PostController>
        [HttpGet]
        public IActionResult Get([FromServices] UseCaseHandler handler,
            [FromServices] IGetPostsQuery query,
            [FromQuery] PostSearch search)
            => Ok(handler.HandleQuery(query, search));

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public IActionResult Get(
            int id,
            [FromServices] UseCaseHandler handler,
            [FromServices] IGetPostQuery query)
            => Ok(handler.HandleQuery(query, id));
        

        // POST api/<PostController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] PostCreateDto data,
            [FromServices] UseCaseHandler handler,
            [FromServices] IWritePostCommand command)
        {
            handler.HandleCommand(command,data);

            return Created();
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
